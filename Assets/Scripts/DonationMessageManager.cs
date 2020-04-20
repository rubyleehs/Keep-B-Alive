using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonationMessageManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float intervalBetweenMessages;
    public float messageFadePeriod;
    public Gradient messageFadeGradient;
    public AudioSource audioSource;

    public AudioClip[] clips;
    public string[] I_requestStrings;
    public string[] I_endMessageStrings;  
    public string[] I_thankStrings;

    private static List<string> messageQueue;
    private static List<float> pointQueue;

    private static string[] thankStrings;
    private static string[] endMessageStrings;
    private static string[] requestStrings;
    private float prevMessageRevealTime, timeSincePrevMessageReveal;

    private void Awake()
    {
        requestStrings = I_requestStrings;
        endMessageStrings = I_endMessageStrings;
        thankStrings = I_thankStrings;
        messageQueue = new List<string>();
        pointQueue = new List<float>();
        prevMessageRevealTime = Time.time + 3f;
    }

    private void Update()
    {
        if (GameManager.isGameOver) return;

        timeSincePrevMessageReveal = Time.time - prevMessageRevealTime;
        
        if (timeSincePrevMessageReveal > intervalBetweenMessages && messageQueue.Count > 0)
        {
            textMesh.text = messageQueue[0];
            GameManager.instance.scoreManager.AddScore(pointQueue[0]);
            timeSincePrevMessageReveal = 0;
            prevMessageRevealTime = Time.time;
            messageQueue.RemoveAt(0);
            pointQueue.RemoveAt(0);
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }

        textMesh.color = messageFadeGradient.Evaluate(timeSincePrevMessageReveal / messageFadePeriod);
    }

    public void QueueMessage(DanceMove move, string username = "")
    {
        if (move.isDone)
        {
            messageQueue.Insert(0, GenerateMessage(move, username));
            pointQueue.Insert(0, move.GetScore());
        }
        else
        {
            messageQueue.Add(GenerateMessage(move, username));
            pointQueue.Add(move.GetScore());
        }
    }

    public string GenerateMessage(DanceMove move, string username = "")
    {
        string output = username;
        if (username.Length <= 0) output = "Anonymous";

        output += " donated $" + move.GetScore() + "!\n";

        if (move.isDone) output += thankStrings[Random.Range(0, thankStrings.Length)];
        else output += requestStrings[Random.Range(0, requestStrings.Length)];
        
        output += " " + move.name + endMessageStrings[Random.Range(0, endMessageStrings.Length)];

        return output;
    }
}
