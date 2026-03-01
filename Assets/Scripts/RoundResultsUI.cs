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
        Hide(); // hide on game start
    }

    public void Hide()
    {
        canvas.enabled = false; // hide
    }

    private void NextRound()
    {
        nextRound = true; // signal to the multiplayer session manager that they can move to the next round
    }

    public void Show(int[] _playerScores, List<Color> _playerColors, int _round, bool _hasPlayerWonGame, int _roundWinner)
    {
        titleText.text = $"Round {_round} Results";
        quitButton.onClick.AddListener(MultiplayerSessionManager.Instance.Quit);
        quit2Button.onClick.AddListener(MultiplayerSessionManager.Instance.Quit);

        for (int i = 0; i < playerStatObjects.Length; i++) // Initialise by disabling each stat object first
        {
            playerStatObjects[i].gameObject.SetActive(false);
        }

        canvas.enabled = true; // show

        for (int i = 0; i < _playerScores.Length; i++) // enable one of the objects for each player tat is playing
        {
            playerStatObjects[i].gameObject.SetActive(true);
            playerStatObjects[i].Initialize(_playerScores[i], _playerColors[i]); // initialise the object
        }

        if (_hasPlayerWonGame) // show the win screen if someone has won
        {
            winScreen.SetActive(true);
        }
        else // hide if not and enable the option to play the next round
        {
            winScreen.SetActive(false);
            nextRoundButton.onClick.AddListener(NextRound);
        }

        winnerText.text = $"PLAYER {_roundWinner + 1} WON ROUND {_round}"; // tailor the win screen to the winner
        winnerText.color = _playerColors[_roundWinner]; // set the color to the color of the player
    }

    public void SetWinText(int _playerIndex, Color color)
    {
        winText.text = $"PLAYER {_playerIndex + 1} HAS WON"; // tailor the win screen to the winner
        winText.color = color; // set the color to the color of the player
    }
}
