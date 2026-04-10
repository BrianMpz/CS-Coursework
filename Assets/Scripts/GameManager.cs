using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// handles and orchestrates all of the gameplay systems
public class GameManager : Singleton<GameManager>
{
    // there will always be a multiplayer session manager if the game is multiplayer
    public bool IsMultiplayer => MultiplayerSessionManager.Instance != null;
    public event Action OnGameStart; // is invoked when the game starts
    public event Action OnSinglePlayerGameEnd; // is invoked when the game ends
    public bool IsPlaying; // true when the game is being played
    [SerializeField] private PlayerObject[] Players; // list of all the prefabricated player objects
    [SerializeField] private List<KeyCode> AlivePlayers; // list of all the currently alive players, identified by their keybind

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Players = Players.OrderBy(x => UnityEngine.Random.value).ToArray(); // shuffle

        if (IsMultiplayer) // init several players 
            InitialiseMultiplayer();
        else // init just one player
            InitialiseSingleplayer();

        StartCoroutine(WaitToStartTheGame()); // starts the coroutine that starts the game
    }

    private void InitialiseSingleplayer()
    {
        // initialise the first player in the list
        Players[0].Initialise(Color.white, KeyCode.Space); // initialise using just plain white and space as a keybind
    }

    private void InitialiseMultiplayer()
    {
        // get all player data
        List<Color> _playerColors = MultiplayerSessionManager.Instance.GetColors();
        List<KeyCode> _playerKeybinds = MultiplayerSessionManager.Instance.GetKeyCodes();

        // iterate through the player keybinds and link an player object to the player and their color
        for (int i = 0; i < _playerKeybinds.Count; i++)
        {
            // get this specific player's data
            PlayerObject _player = Players[i];
            Color _playerColor = _playerColors[i];
            KeyCode _playerKeycode = _playerKeybinds[i];

            // initialise player object
            _player.Initialise(_playerColor, _playerKeycode);
        }

        AlivePlayers = new(_playerKeybinds); // set the alive players to the initial amount of players
    }

    private IEnumerator WaitToStartTheGame()
    {
        IsPlaying = false; // initialise isplaying to false
        // prompt player to start the game
        GameUI.Instance.ShowPrompt("Press [Space] to Start the Game"); // prompt the player to start the game

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // wait until space has been pressed

        // prompt player to start the game
        GameUI.Instance.HidePrompt();

        OnGameStart?.Invoke(); // call the event to signal subscribers of the event
        IsPlaying = true; // set isplaying to true
    }

    public IEnumerator OnPlayerDeath(PlayerObject _player)
    {
        Destroy(_player.gameObject); // destroy the player object

        if (IsMultiplayer)
        {
            if (AlivePlayers.Count == 1) yield break; // ignore if theres only one player left

            AlivePlayers.RemoveAt(AlivePlayers.IndexOf(_player.myKeyBind)); // remove player from the list of alive player

            if (AlivePlayers.Count == 1) // if theres only one player left
            {
                yield return GameEndVisual(); // play game over visual
                MultiPlayerEndGame(AlivePlayers[0]); // handle the multiplayer game ending
            }
        }
        else
        {
            yield return GameEndVisual(); // play game over visual
            SinglePlayerEndGame(); // handle the singleplayer game ending
        }
    }

    private IEnumerator GameEndVisual()
    {
        float _pulseDuration = 1.5f; // how long the pulse lasts
        float _timer = 0f; // timer to keep track of how long has passed
        Color _baseBGColor = new(21f / 255, 46f / 255, 80f / 255); // the base color of the bg

        float _baseScrollRate = ObstacleGenerator.Instance.scrollRate; // get the initial scrol rate

        while (_timer < _pulseDuration)
        {
            Camera.main.backgroundColor = Color.Lerp(Color.red, _baseBGColor, Mathf.Pow(_timer / _pulseDuration, 2)); // use linear interpolation
            ObstacleGenerator.Instance.scrollRate = Mathf.Lerp(_baseScrollRate, 0, _timer / _pulseDuration); // use linear interpolation

            // add to timer
            _timer += Time.deltaTime * ScoreManager.Instance.GameSpeed; // add to the timer
            yield return null; // wait for a frame
        }

        yield return new WaitForSeconds(.5f); // wait 0.5s
    }

    public void SinglePlayerEndGame()
    {
        OnSinglePlayerGameEnd?.Invoke(); // notify suybscribers
        IsPlaying = false;
    }

    public void MultiPlayerEndGame(KeyCode _key)
    {
        MultiplayerSessionManager.Instance.EndMultiplayerGame(_key); // return flow back to the multiplayer session manager
        IsPlaying = false;
    }
}
