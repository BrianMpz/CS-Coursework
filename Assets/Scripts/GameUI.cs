using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// handles the game UI
public class GameUI : Singleton<GameUI>
{
    [SerializeField] private Button SkipButton;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text PromptText;
    [SerializeField] private Button AddPointButton;

    private void Update()
    {
        ScoreText.text = ScoreManager.Instance.GetScore().ToString("N0"); // formatted to have commas
    }

    public void ShowPrompt(string _message)
    {
        PromptText.enabled = true; // show prompt
        PromptText.text = _message; // set prompt to parameter
    }

    public void HidePrompt()
    {
        PromptText.enabled = false; // hide prompt
    }
}
