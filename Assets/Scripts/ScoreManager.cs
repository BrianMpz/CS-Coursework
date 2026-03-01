using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int currentScore;
    private int highScore;

    void Awake()
    {
        GameManager.Instance.OnGameStart += Initialize; // initialise when the game starts
    }

    private void Initialize()
    {
        currentScore = 0;
        highScore = DataManager.GetIntFromSave("High Score"); // retrieve from memory

        print($"Score: {currentScore:N0}"); // formatted to have commas
        print($"Best: {highScore:N0}"); // formatted to have commas
    }

    public int GetScore() => currentScore;
    public int GetHighScore() => highScore;
    public void IncrementScore()
    {
        currentScore += 1;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            DataManager.SaveInt("High Score", currentScore); // save to memory
        }
    }
}
