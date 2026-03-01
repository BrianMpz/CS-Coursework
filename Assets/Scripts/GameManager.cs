using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsMultiplayer => MultiplayerSessionManager.Instance != null;

    public event Action OnGameStart;
    public event Action OnMultiPlayerGameEnd;
    public event Action OnSinglePlayerGameEnd;
    public bool IsPlaying;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitToStartTheGame());
    }

    private IEnumerator WaitToStartTheGame()
    {
        IsPlaying = false;
        // prompt player to start the game
        GameUI.Instance.ShowPrompt("Press [Space] to Start the Game");

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        // prompt player to start the game
        GameUI.Instance.HidePrompt();

        OnGameStart?.Invoke();
        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OnPlayerDeath(PlayerObject player)
    {
        Destroy(player.gameObject); // destroy the player
        print("Player Has Died");

        yield return new WaitForSeconds(1); // wait for a second

        EndGame();
    }

    public void EndGame()
    {
        print("The Game Has ended");
        OnSinglePlayerGameEnd?.Invoke(); // notify suybscribers
        IsPlaying = false;
    }
}
