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
    public int remainingPlayerIndex;

    public void Initialize(List<Color> _playerColors, List<KeyCode> _playerKeybinds, int _numberOfPlayers)
    {
        if (_playerKeybinds.Count != _numberOfPlayers)
            Debug.LogError("Internal Error: number of players doesn not match the amount of keybinds!"); // throw an error if there is a discrepancy between the amount of players and keybinds

        // initualise values
        PlayerColors = _playerColors;
        playerKeybinds = _playerKeybinds;
        playerScores = new int[_numberOfPlayers];

        DontDestroyOnLoad(gameObject); // persist between scenes

        StartCoroutine(StartRoundPlayingLoop()); //  start the gameplay loop
    }

    public List<Color> GetColors() => PlayerColors;
    public List<KeyCode> GetKeyCodes() => playerKeybinds;
    public int GetNumPlayers() => playerScores.Length;

    private IEnumerator StartRoundPlayingLoop() // starts rouund, waits for the round to end and awards the player a point. check for an overall winner
    {
        rounds = 0;

        while (true)
        {
            rounds++; // increment number of rounds
            remainingPlayerIndex = -1; // nobody can be the remaining player yet

            StartRound();

            yield return new WaitUntil(() => hasLoadedGameScene); // wait until the game has loaded

            print($"Round {rounds} has started!");

            yield return new WaitUntil(() => hasRoundEnded == true && remainingPlayerIndex != -1); // wait until the round has ended

            print($"Round {rounds} has ended!");

            int finalPlayer = remainingPlayerIndex; // get the amount of players remaining

            print($"Player {finalPlayer + 1} has won");

            AwardPlayerAPoint(finalPlayer); // award this winner a point

            ShowRoundResults(finalPlayer); // display results to the users

            if (HasPlayerWon()) // end the cycle if a player reaches the win thresholds
            {
                RoundResultsUI.Instance.SetWinText(finalPlayer, PlayerColors[finalPlayer]); // display to user
                yield break; // break out
            }

            yield return new WaitUntil(() => RoundResultsUI.Instance.nextRound); // wait until green light is given to move on
        }
    }

    private void StartRound()
    {
        hasRoundEnded = false;
        Loader.LoadScene(Scene.GameScene); // call the loader to load the scene
        SceneManager.sceneLoaded += LoadedScene; // listen to when this scene is loaded
        hasLoadedGameScene = false;
    }

    private void LoadedScene(UnityEngine.SceneManagement.Scene _, LoadSceneMode __)
    {
        SceneManager.sceneLoaded -= LoadedScene; // unsubscribe
        hasLoadedGameScene = true; // close flag to confirm that it has loaded
    }

    public void EndMultiplayerGame(KeyCode _key)
    {
        playerKeybinds.ForEach(x => Debug.Log(x));

        remainingPlayerIndex = playerKeybinds.IndexOf(_key);
        hasRoundEnded = true;
    }

    private bool HasPlayerWon()
    {
        // go though each player score and check if theyve met the threshold
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

    private void ShowRoundResults(int _player) // display results
    {
        RoundResultsUI.Instance.Show(playerScores, PlayerColors, rounds, HasPlayerWon(), _player);
    }

    public void Quit() // quit to main menu
    {
        Destroy(gameObject); // discard this singleton
        Loader.LoadScene(Scene.MainMenu);
    }
}