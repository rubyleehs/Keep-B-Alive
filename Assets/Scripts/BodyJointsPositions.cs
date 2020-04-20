using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BodyJointsPositions
{
    public Vector2 buttPos;
    public Vector2 leftHipPos, rightHipPos;
    public Vector2 leftKneePos, rightKneePos;
    public Vector2 leftFeetPos, rightFeetPos;

    public BodyJointsPositions(Vector2 buttPos, Vector2 leftHipPos, Vector2 rightHipPos, Vector2 leftKneePos, Vector2 rightKneePos, Vector2 leftFeetPos, Vector2 rightFeetPos)
    {
        this.buttPos = buttPos;
        this.leftHipPos = leftHipPos;
        this.rightHipPos = rightHipPos;
        this.leftKneePos = leftKneePos;
        this.rightKneePos = rightKneePos;
        this.leftFeetPos = leftFeetPos;
        this.rightFeetPos = rightFeetPos;
    }

    public static BodyJointsPositions operator +(BodyJointsPositions a, BodyJointsPositions b)
    {
        return new BodyJointsPositions(
            a.buttPos + b.buttPos,
            a.leftHipPos + b.leftHipPos, a.rightHipPos + b.rightHipPos,
            a.leftKneePos + b.leftKneePos, a.rightKneePos + b.rightKneePos,
            a.leftFeetPos + b.leftFeetPos, a.rightFeetPos + b.rightFeetPos
            );
    }

    public static BodyJointsPositions operator -(BodyJointsPositions a, BodyJointsPositions b)
    {
        return new BodyJointsPositions(
            a.buttPos - b.buttPos,
            a.leftHipPos - b.leftHipPos, a.rightHipPos - b.rightHipPos,
            a.leftKneePos - b.leftKneePos, a.rightKneePos - b.rightKneePos,
            a.leftFeetPos - b.leftFeetPos, a.rightFeetPos - b.rightFeetPos
            );
    }

    public static BodyJointsPositions operator *(BodyJointsPositions a, float b)
    {
        BodyJointsPositions temp = 
            new BodyJointsPositions(
            a.buttPos * b,
            a.leftHipPos * b, a.rightHipPos * b,
            a.leftKneePos * b, a.rightKneePos * b,
            a.leftFeetPos * b, a.rightFeetPos * b
            );
        return temp;
    }
}
