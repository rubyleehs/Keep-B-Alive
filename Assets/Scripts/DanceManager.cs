using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceManager : MonoBehaviour
{
    public int maxMoveGenerationAttempts, maxDancePartPerMove;
    public int maxDanceMoves;
    public Vector2 newDanceMoveInterval;
    public DancePart[] danceParts;

    private float nextDanceMoveAdditionTime;

    private List<DanceMove> currentDanceMoves;

    private void Awake()
    {
        currentDanceMoves = new List<DanceMove>();
    }

    private void Update()
    {
        if (GameManager.isGameOver) return;
        if (Time.time < nextDanceMoveAdditionTime) return;

        nextDanceMoveAdditionTime = Time.time + Random.Range(newDanceMoveInterval.x, newDanceMoveInterval.y);

        if (!GameManager.instance.viewsManager.IsDonationIncoming()) return;
        
        currentDanceMoves.Add(GenerateNewDanceMove());
        GameManager.instance.donationMessageManager.QueueMessage(currentDanceMoves[currentDanceMoves.Count - 1]);

        while (currentDanceMoves.Count > maxDanceMoves)
        {
            currentDanceMoves.RemoveAt(0);
            GameManager.instance.viewsManager.DecreaseViews();
        }
    }

    public void CheckAnyDanceMoveSuccess(Body body)
    {
        DanceMove danceMove;
        for(int i = 0; i < currentDanceMoves.Count; i++)
        {
            danceMove = currentDanceMoves[i];
            if (danceMove.isDone) continue;
            if (danceMove.CheckSuccess(body))
            {
                danceMove.isDone = true;
                Debug.Log("Performed " + danceMove.name + "earning " + danceMove.GetScore() + " points");
                GameManager.instance.donationMessageManager.QueueMessage(danceMove);
                GameManager.instance.viewsManager.IncreaseViews();
                currentDanceMoves.RemoveAt(i);
                i--;
            }
        }
    }

    public DanceMove GenerateNewDanceMove()
    {
        DanceMove output = new DanceMove();
        int attempts = 0, parts = 0;

        while(attempts < maxMoveGenerationAttempts && parts < maxDancePartPerMove)
        {
            attempts++;
            if (output.TryAddDancePart(danceParts[Random.Range(0, danceParts.Length)])) parts++;
        }

        return output;
    }
}
