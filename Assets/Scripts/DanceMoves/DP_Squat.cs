using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DP_Squat", menuName = "DancePart/Squat")]
public class DP_Squat : DancePart
{
    public Vector2 sqrDistanceBetweenFeetBounds;
    public Vector2 sqrDistanceBetweenKneesBounds;
    public Vector2 buttDistanceFromGroundBounds;

    public override bool CheckSuccess(Body body)
    {
        float feetSqrDisplacement = Vector2.SqrMagnitude(
            body.GetMeanJointsPositions().leftFeetPos - body.GetMeanJointsPositions().rightFeetPos);
        float kneesSqrDisplacement = Vector2.SqrMagnitude(
           body.GetMeanJointsPositions().leftKneePos - body.GetMeanJointsPositions().rightKneePos);
        float buttDistFromGround = body.GetMeanJointsPositions().buttPos.y - body.GetMeanJointsPositions().leftFeetPos.y;
        //Debug.Log("SqrDist Between Both Feet: " + feetSqrDisplacement + "Butt Dist Off ground: " + buttDistFromGround + "Knees: " + kneesSqrDisplacement);

        return (feetSqrDisplacement > sqrDistanceBetweenFeetBounds.x && feetSqrDisplacement < sqrDistanceBetweenFeetBounds.y 
            && kneesSqrDisplacement > sqrDistanceBetweenKneesBounds.x && kneesSqrDisplacement < sqrDistanceBetweenKneesBounds.y
            && buttDistFromGround > buttDistanceFromGroundBounds.x && buttDistFromGround < buttDistanceFromGroundBounds.y);
    }
}
