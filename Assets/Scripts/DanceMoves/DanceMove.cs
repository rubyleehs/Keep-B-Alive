using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMove 
{
    public string name; 
    public bool isDone;

    private HashSet<DanceBodyPart> requiredBodyParts;
    private List<DancePart> danceParts;
    private float score, difficulty;

    public DanceMove()
    {
        isDone = false;
        requiredBodyParts = new HashSet<DanceBodyPart>();
        danceParts = new List<DancePart>();
    }

    public bool CheckSuccess(Body body)
    {
        for(int i = 0; i < danceParts.Count; i++)
        {
            if (!danceParts[i].CheckSuccess(body)) return false;
        }
        return true;
    }

    public float GetScore()
    {
        float s = score * difficulty;
        if (!isDone) s *= GameManager.instance.scoreManager.moveRequestScoreMultiplier;
        return Mathf.Round(s * 100) * 0.01f;
    }

    public bool TryAddDancePart(DancePart dancePart)
    {
        if (requiredBodyParts.Contains(dancePart.bodyPart)) return false;

        requiredBodyParts.Add(dancePart.bodyPart);
        danceParts.Add(dancePart);
        score += dancePart.baseScore;
        difficulty += dancePart.difficulty;
        name += dancePart.name + " ";

        return true;
    }
}

public abstract class DancePart: ScriptableObject
{
    public new string name;
    public DanceBodyPart bodyPart;
    public float difficulty;
    public float baseScore;

    public abstract bool CheckSuccess(Body body);
}

public enum DanceBodyPart { Butt, Legs};
