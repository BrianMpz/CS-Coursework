using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button SkipButton;

    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private Button AddPointButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isMultiplayer = GameManager.Instance.IsMultiplayer;

        if (isMultiplayer)
        {
            AddPointButton.gameObject.SetActive(false);
            SkipButton.onClick.AddListener(MultiplayerSessionManager.Instance.SkipGame);
        }
        else
        {
            AddPointButton.gameObject.SetActive(true);
            AddPointButton.onClick.AddListener(ScoreManager.Instance.IncrementScore);
            SkipButton.onClick.AddListener(GameManager.Instance.OnPlayerDeath);
        }
    }

    private void Update()
    {
        ScoreText.text = ScoreManager.Instance.GetScore().ToString();
    }
}
