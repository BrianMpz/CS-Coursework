using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] private Button SkipButton;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text PromptText;
    [SerializeField] private Button AddPointButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // setup the UI differently based on if its multiplayer
        if (GameManager.Instance.IsMultiplayer) // multiplayer
        {
            SkipButton.onClick.AddListener(MultiplayerSessionManager.Instance.SkipGame);
        }
        else // singleplayer 
        {
            AddPointButton.onClick.AddListener(ScoreManager.Instance.IncrementScore);
            SkipButton.onClick.AddListener(GameManager.Instance.EndGame);
        }
    }

    private void Update()
    {
        ScoreText.text = ScoreManager.Instance.GetScore().ToString("N0"); // formatted to have commas
    }

    public void ShowPrompt(string message)
    {
        PromptText.enabled = true; // show prompt
        PromptText.text = message; // set prompt to parameter
    }

    public void HidePrompt()
    {
        PromptText.enabled = false; // hide prompt
    }
}
