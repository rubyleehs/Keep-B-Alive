using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float moveRequestScoreMultiplier = 0.05f;

    [HideInInspector]
    public float currentScore;

    public Gradient scoreShrinkGradient;
    public Vector2 scoreFontSizeBounds;
    public float scoreSizeShrinkPeriod;

    public TextMeshProUGUI scoreText;

    private IEnumerator scoreSizeRoutine;

    public void AddScore(float amount)
    {
        currentScore += amount;
        scoreText.text = "$" + ((int)(currentScore * 100))*0.01f;

        if (scoreSizeRoutine != null) StopCoroutine(scoreSizeRoutine);

        scoreSizeRoutine = ShrinkScoreText();
        StartCoroutine(scoreSizeRoutine);
    }

    public IEnumerator ShrinkScoreText()
    {
        float startTime = Time.time;
        float progress = 0;
        while(progress <= 1)
        {
            progress = Mathf.Lerp(0, 1, (Time.time - startTime) / scoreSizeShrinkPeriod);
            scoreText.fontSize = Mathf.SmoothStep(scoreFontSizeBounds.y, scoreFontSizeBounds.x, 1 - Mathf.Pow(1 - progress, 3));
            scoreText.color = scoreShrinkGradient.Evaluate(progress);

            yield return new WaitForEndOfFrame();
        }

        scoreText.fontSize = scoreFontSizeBounds.x;
        scoreSizeRoutine = null;
    }
}
