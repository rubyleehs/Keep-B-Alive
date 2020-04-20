using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DP_Split", menuName = "DancePart/Split")]
public class DP_Split : DancePart
{
    public float minSqrDistanceBetweenFeet;

    public override bool CheckSuccess(Body body)
    {
        float sqrDisplacement = Vector2.SqrMagnitude(
            body.GetMeanJointsPositions().leftFeetPos - body.GetMeanJointsPositions().rightFeetPos);
        //Debug.Log("SqrDist Between Both Feet: " + sqrDisplacement + ". Need at least: " + minSqrDistanceBetweenFeet);
        ////Debug.Log("Current Sqr Dist Between Both Feet: " + Vector2.SqrMagnitude(body.current.leftFeetPos - body.current.rightFeetPos));
        return (sqrDisplacement >= minSqrDistanceBetweenFeet);
    }
}
