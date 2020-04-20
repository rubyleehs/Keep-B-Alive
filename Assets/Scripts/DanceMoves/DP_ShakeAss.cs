using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DP_ShakeAss", menuName = "DancePart/ShakeAss")]
public class DP_ShakeAss : DancePart
{
    public int minButtPassesThroughMid;
    public float maxYSqrDisplacementFromMid;

    public override bool CheckSuccess(Body body)
    {
        Vector2 mid = body.GetMeanJointsPositions().buttPos;

        int buttPasses = 0;
        for (int i = 1; i < body.historyLog.Count; i++)
        {
            if (Mathf.Pow(body.historyLog[i].buttPos.y - mid.y, 2) > maxYSqrDisplacementFromMid)
            {
                //Debug.Log("Butt Passes Reset!");
                buttPasses = 0;
            }
            if ((body.historyLog[i - 1].buttPos.x - mid.x) * (body.historyLog[i].buttPos.x - mid.x) < 0)
            {
                buttPasses++;
                if (buttPasses >= minButtPassesThroughMid)
                {
                    //Debug.Log("Butt Passes: " + buttPasses);
                    return true;
                }
            }
        }
        //Debug.Log("Butt Passes: " + buttPasses);
        return false;
    }
}