using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Managers")]
    public ScoreManager scoreManager;
    public DanceManager danceManager;
    public ViewsManager viewsManager;
    public DonationMessageManager donationMessageManager;

    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverText;

    public static bool isGameOver;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        
        instance = this;
        isGameOver = false;
        gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        gameOverText.text += "$" + scoreManager.currentScore;
    }
}
