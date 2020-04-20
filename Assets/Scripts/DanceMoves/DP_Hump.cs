using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DP_Hump", menuName = "DancePart/Hump")]
public class DP_Hump : DancePart
{
    public int minButtPassesThroughMid;
    public float maxXSqrDisplacementFromMid;

    public override bool CheckSuccess(Body body)
    {
        Vector2 mid = body.GetMeanJointsPositions().buttPos;

        int buttPasses = 0;
        for(int i = 1; i < body.historyLog.Count; i++)
        {
            if (Mathf.Pow(body.historyLog[i].buttPos.x - mid.x, 2) > maxXSqrDisplacementFromMid)
            {
                //Debug.Log("Butt Passes Reset!");
                buttPasses = 0;
            }
            if ((body.historyLog[i - 1].buttPos.y - mid.y) * (body.historyLog[i].buttPos.y - mid.y) < 0)
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
