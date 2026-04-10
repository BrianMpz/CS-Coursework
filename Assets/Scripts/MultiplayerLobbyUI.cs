// Manages UI Logic for the Lobby
// Fulfills criteria 9
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manages all multiplayer lobby UI logic
public class MultiplayerLobbyUI : Singleton<MultiplayerLobbyUI>
{
    private const int MAX_NUMBER_OF_PLAYERS = 5;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private int numberOfPlayers; // keeps track of the number of players
    public List<Color> PlayerColors; // kepps track of the player's color
    [SerializeField] private List<KeyCode> playerKeybinds; // keeps track of the player's keybinds
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements
    [SerializeField] private GameObject SetKeybindScreen; // screen that appears when setting keybinds

    [SerializeField] private Button BackButton; // kepps track of the player's color
    [SerializeField] private Button PlayButton; // kepps track of the player's color

    public event Action OnKeybindsChanged; // event for when a player's keybind is changed
    public event Action<int> OnNumberOfPlayersChanged; // event for when the number of players is changed
    private bool hasSetDefaultPlayers;

    private void Start()
    {
        Show(); // this screen should always shw throughout the scene

        SetNumberOfPlayersPlaying(MIN_NUMBER_OF_PLAYERS); // reset to default

        SetKeybindScreen.SetActive(false); // hide the set keybind screen

        BackButton.onClick.AddListener(OnBackButtonPressed); // leave on button press
        PlayButton.onClick.AddListener(OnPlayButtonPressed); // start on button press
    }

    private void Update()
    {
        if (!hasSetDefaultPlayers) // set default players on the first frame to avoid issues with order of operations
        {
            SetNumberOfPlayersPlaying(MIN_NUMBER_OF_PLAYERS);
            hasSetDefaultPlayers = true;
        }
    }

    private void Show()
    {
        Canvas.enabled = true; // enable UI
    }

    private void SetKeyBindForPlayer(KeyCode _keybind, int _player) // updates the list to the player's preferred keybind
    {
        playerKeybinds[_player] = _keybind; // change keybind at specific index of the player
        OnKeybindsChanged?.Invoke(); // allert listeners to this change
    }

    public IEnumerator OnKeyBindSetButtonPressed(int _player)
    {
        SetKeybindScreen.SetActive(true); // show the set keybind screen

        List<KeyCode> _excludedKeybinds = new(playerKeybinds);
        _excludedKeybinds.RemoveAt(_player); // remove the current player's keybind from the excluded list

        yield return new WaitUntil(() => KeybindManager.HasALegalKeybindBeenPressed(_excludedKeybinds)); // wait until a legal keybind is pressed

        foreach (KeyCode _keycode in KeybindManager.GetLegalKeybinds()) // go through each legal and if its down then we set it
        {
            if (Input.GetKeyDown(_keycode))
            {
                SetKeyBindForPlayer(_keycode, _player);
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
        PlayButton.gameObject.SetActive(false);
        MultiplayerSessionManager.Instance.Initialize(PlayerColors, playerKeybinds, numberOfPlayers);
    }

    public void SetNumberOfPlayersPlaying(int _newValue)
    {
        if (_newValue > MAX_NUMBER_OF_PLAYERS || _newValue < MIN_NUMBER_OF_PLAYERS) return; // cant set higher than the max

        numberOfPlayers = _newValue; // set to the new value

        while (playerKeybinds.Count < numberOfPlayers) // pad out unset keybinds with nothing
        {
            playerKeybinds.Add(KeyCode.None);
            int _addedIndex = playerKeybinds.Count - 1;
            playerKeybinds[_addedIndex] = KeyCode.Alpha1 + _addedIndex; // default keybinds are 1, 2, 3, 4, 5
        }

        // alert listeners
        OnNumberOfPlayersChanged?.Invoke(numberOfPlayers);
        OnKeybindsChanged?.Invoke();
    }

    public KeyCode GetKeybindForPlayer(int _player)
    {
        return playerKeybinds[_player];
    }
}