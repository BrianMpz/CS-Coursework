// Manages the multiplayer session by loading the game scenes and keeping track of how many games each player has won
// Fulfills criteria 9, 1
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerSessionManager : Singleton<MultiplayerSessionManager>
{
    public const int WIN_THRESHOLD = 5;
    private int[] playerScores;
    private List<Color> PlayerColors;
    private List<KeyCode> playerKeybinds;
    private bool hasRoundEnded;
    private bool hasLoadedGameScene;
    public int rounds;

    public void Initialize(List<Color> _playerColors, List<KeyCode> _playerKeybinds, int _numberOfPlayers)
    {
        if (_playerKeybinds.Count != _numberOfPlayers)
            Debug.LogError("Internal Error: number of players doesn not match the amount of keybinds!");

        PlayerColors = _playerColors;
        playerKeybinds = _playerKeybinds;
        playerScores = new int[_numberOfPlayers];

        DontDestroyOnLoad(gameObject); // persist between scenes

        StartCoroutine(StartRoundPlayingLoop());
    }

    private IEnumerator StartRoundPlayingLoop() // starts rouund, waits for the round to end and awards the player a point. check for an overall winner
    {
        rounds = 0;

        while (true)
        {
            rounds++;

            StartRound();

            yield return new WaitUntil(() => hasLoadedGameScene);

            Debug.Log($"Round {rounds} has started!");

            yield return new WaitUntil(() => hasRoundEnded == true);

            Debug.Log($"Round {rounds} has ended!");

            int finalPlayer = GetRemainingPlayerIndex();

            Debug.Log($"Player {finalPlayer + 1} has won");

            AwardPlayerAPoint(finalPlayer);

            ShowRoundResults(finalPlayer);

            if (HasPlayerWon())
            {
                RoundResultsUI.Instance.SetWinText(finalPlayer, PlayerColors[finalPlayer]);
                yield break;
            }

            yield return new WaitUntil(() => RoundResultsUI.Instance.nextRound);
        }
    }

    private void StartRound()
    {
        hasRoundEnded = false;
        Loader.LoadScene(Scene.GameScene);

        SceneManager.sceneLoaded += LoadedScene;
        hasLoadedGameScene = false;
    }

    private void LoadedScene(UnityEngine.SceneManagement.Scene _, LoadSceneMode __)
    {
        SceneManager.sceneLoaded -= LoadedScene;
        hasLoadedGameScene = true;
    }

    public void SkipGame() // TODO: remove later
    {
        hasRoundEnded = true;
    }

    private int GetRemainingPlayerIndex()
    {
        return UnityEngine.Random.Range(0, playerKeybinds.Count);
    }

    private bool HasPlayerWon()
    {
        foreach (int playerScore in playerScores) // enumerates through each player and checks if the have won
        {
            if (playerScore == WIN_THRESHOLD)
            {
                return true;
            }
        }
        return false;
    }

    private void AwardPlayerAPoint(int _playerIndex)
    {
        playerScores[_playerIndex]++; // awards the correct player a point based off their index
    }

    private void ShowRoundResults(int _player)
    {
        RoundResultsUI.Instance.Show(playerScores, PlayerColors, rounds, HasPlayerWon(), _player);
    }

    public void Quit()
    {
        Destroy(gameObject);
        Loader.LoadScene(Scene.MainMenu);
    }
}