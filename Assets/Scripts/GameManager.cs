using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsMultiplayer => MultiplayerSessionManager.Instance != null;

    public event Action OnGameStart;
    public event Action OnMultiPlayerGameEnd;
    public event Action OnSinglePlayerGameEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnGameStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player Has Died and the Game Has ended");
        OnSinglePlayerGameEnd?.Invoke();
    }
}
