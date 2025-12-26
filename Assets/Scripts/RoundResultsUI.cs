using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class RoundResultsUI : Singleton<RoundResultsUI>
{
    [SerializeField] private PlayerStatObject[] playerStatObjects;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button nextRoundButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button quit2Button;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text winnerText;
    public bool nextRound;

    private void Start()
    {
        Hide();
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    private void NextRound()
    {
        nextRound = true;
    }

    public void Show(int[] _playerScores, List<Color> _playerColors, int _round, bool _hasPlayerWonGame, int _roundWinner)
    {
        titleText.text = $"Round {_round} Results";
        quitButton.onClick.AddListener(MultiplayerSessionManager.Instance.Quit);
        quit2Button.onClick.AddListener(MultiplayerSessionManager.Instance.Quit);

        for (int i = 0; i < playerStatObjects.Length; i++)
        {
            playerStatObjects[i].gameObject.SetActive(false);
        }

        canvas.enabled = true;

        for (int i = 0; i < _playerScores.Length; i++)
        {
            playerStatObjects[i].gameObject.SetActive(true);
            playerStatObjects[i].Initialize(_playerScores[i], _playerColors[i]);
        }

        if (_hasPlayerWonGame)
        {
            winScreen.SetActive(true);
        }
        else
        {
            winScreen.SetActive(false);
            nextRoundButton.onClick.AddListener(NextRound);
        }

        winnerText.text = $"PLAYER {_roundWinner + 1} WON ROUND {_round}";
        winnerText.color = _playerColors[_roundWinner];
    }

    public void SetWinText(int _playerIndex, Color color)
    {
        winText.text = $"PLAYER {_playerIndex + 1} HAS WON";
        winText.color = color;
    }
}
