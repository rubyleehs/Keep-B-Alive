using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DP_Stand", menuName = "DancePart/Stand")]
public class DP_Stand : DancePart
{
    public float maxSqrDistanceBetweenFeet;
    public Vector2 buttDistanceFromGroundBounds;

    public override bool CheckSuccess(Body body)
    {
        float feetSqrDisplacement = Vector2.SqrMagnitude(
            body.GetMeanJointsPositions().leftFeetPos - body.GetMeanJointsPositions().rightFeetPos);
        float buttDistFromGround = body.GetMeanJointsPositions().buttPos.y - body.GetMeanJointsPositions().leftFeetPos.y;
        //Debug.Log("SqrDist Between Both Feet: " + feetSqrDisplacement + "Butt Dist Off ground: " + buttDistFromGround);
        
        return (feetSqrDisplacement < maxSqrDistanceBetweenFeet && buttDistFromGround > buttDistanceFromGroundBounds.x && buttDistFromGround < buttDistanceFromGroundBounds.y);
    }
}
