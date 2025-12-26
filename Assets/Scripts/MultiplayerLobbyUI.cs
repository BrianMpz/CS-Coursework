// Manages UI Logic for the Lobby
// Fulfills criteria 9
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerLobbyUI : Singleton<MultiplayerLobbyUI>
{
    private const int MAX_NUMBER_OF_PLAYERS = 5;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private int numberOfPlayers; // keeps track of the number of players
    public List<Color> PlayerColors; // kepps track of the player's color
    [SerializeField] private List<KeyCode> playerKeybinds; // keeps track of the player's keybinds
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements
    [SerializeField] private GameObject SetKeybindScreen; // screen that appears when setting keybinds

    [SerializeField] private Button backButton; // kepps track of the player's color
    [SerializeField] private Button playButton; // kepps track of the player's color

    public event Action OnKeybindsChanged; // event for when a player's keybind is changed
    public event Action<int> OnNumberOfPlayersChanged; // event for when the number of players is changed

    private void Start()
    {
        Show();

        SetNumberOfPlayersPlaying(MIN_NUMBER_OF_PLAYERS);

        SetKeybindScreen.SetActive(false); // hide the set keybind screen

        backButton.onClick.AddListener(OnBackButtonPressed);
        playButton.onClick.AddListener(OnPlayButtonPressed);
    }

    private void Show()
    {
        Canvas.enabled = true; // enable UI
    }

    private void SetKeyBindForPlayer(KeyCode _keybind, int _player) // updates the list to the player's preferred keybind
    {
        playerKeybinds[_player] = _keybind;
        OnKeybindsChanged?.Invoke();
    }

    public IEnumerator OnKeyBindSetButtonPressed(int _player)
    {
        SetKeybindScreen.SetActive(true); // show the set keybind screen

        yield return new WaitUntil(() => KeybindManager.HasALegalKeybindBeenPressed()); // wait until a legal keybind is pressed

        foreach (KeyCode keycode in KeybindManager.GetLegalKeybinds())
        {
            if (Input.GetKeyDown(keycode))
            {
                SetKeyBindForPlayer(keycode, _player);
                SetKeybindScreen.SetActive(false); // hide the set keybind screen
                break;
            }
        }
    }

    private void OnBackButtonPressed()
    {
        Loader.LoadScene(Scene.MainMenu); // go back to main menu
    }

    private void OnPlayButtonPressed() // start the game
    {
        playButton.gameObject.SetActive(false);
        MultiplayerSessionManager.Instance.Initialize(PlayerColors, playerKeybinds, numberOfPlayers);
    }

    public void SetNumberOfPlayersPlaying(int _newValue)
    {
        if (_newValue > MAX_NUMBER_OF_PLAYERS || _newValue < MIN_NUMBER_OF_PLAYERS) return; // cant set higher than the max

        numberOfPlayers = _newValue;

        while (playerKeybinds.Count < numberOfPlayers)
        {
            playerKeybinds.Add(KeyCode.None);
            int addedIndex = playerKeybinds.Count - 1;
            playerKeybinds[addedIndex] = KeyCode.Alpha1 + addedIndex; // default keybinds are 1, 2, 3, 4, 5
        }

        OnNumberOfPlayersChanged?.Invoke(numberOfPlayers);
        OnKeybindsChanged?.Invoke();
    }

    public KeyCode GetKeybindForPlayer(int _player)
    {
        return playerKeybinds[_player];
    }
}