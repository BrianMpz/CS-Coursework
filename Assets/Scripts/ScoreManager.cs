using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int currentScore;
    private int highScore;

    void Awake()
    {
        GameManager.Instance.OnGameStart += Initialize; // initialise when the game starts
    }

    private void Initialize()
    {
        currentScore = 0;
        highScore = DataManager.GetIntFromSave("High Score"); // retrieve from memory
        Debug.Log($"Score: {currentScore}");
        Debug.Log($"Best: {highScore}");
    }

    public int GetScore() => currentScore;
    public int GetHighScore() => highScore;
    public void IncrementScore()
    {
        currentScore++;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            DataManager.SaveInt("High Score", currentScore); // save to memory
        }
    }
}
