using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    int lives;
    int totalCoins;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text HighScoreText;

    void Start()
    {
        lives = FindObjectOfType<GameSession>().GetLives();
        totalCoins = FindObjectOfType<GameSession>().GetTotalScore();
        livesText.text = lives.ToString();
        scoreText.text = totalCoins.ToString();

        if(totalCoins > PlayerPrefs.GetInt("HighScore") || PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", totalCoins);
        }
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        Time.timeScale = 0;
    }
}
