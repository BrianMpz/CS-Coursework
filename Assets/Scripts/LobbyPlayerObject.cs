using TMPro;
using UnityEngine;
using UnityEngine.UI;

// player model in the lobby
public class LobbyPlayerObject : MonoBehaviour
{
    [SerializeField] private int PlayerID; // the ID of the player
    [SerializeField] private GameObject PlayerModel; // the model of the player
    [SerializeField] private TMP_Text PlayerKeybind; // the keybind of the player
    [SerializeField] private Button SetKeybindButton; // the button of the player

    private void Start()
    {
        SetKeybindButton.onClick.AddListener(SetKeyBindForPlayer);
        MultiplayerLobbyUI.Instance.OnKeybindsChanged += OnKeybindsChanged;
        MultiplayerLobbyUI.Instance.OnNumberOfPlayersChanged += OnNumberOfPlayersChanged;
        SetColor();
    }

    private void SetColor() // go though all images in the player model and set their color to the player's color
    {
        Color _color = MultiplayerLobbyUI.Instance.PlayerColors[PlayerID];
        Image[] _playerImages = PlayerModel.GetComponentsInChildren<Image>();

        // goes through each part of the player model and sets it to the color
        foreach (Image _image in _playerImages)
        {
            _image.color = _color;
        }
    }

    private void OnKeybindsChanged() // update the keybind text
    {
        if (!gameObject.activeSelf) return;

        KeyCode _currentKeybind = MultiplayerLobbyUI.Instance.GetKeybindForPlayer(PlayerID);
        PlayerKeybind.text = $"[{KeybindManager.KeyCodeToString(_currentKeybind)}]";
    }

    private void OnNumberOfPlayersChanged(int _newNumberOfPlayers) // enable or disable the player object based on the number of players
    {
        bool _enabled = PlayerID < _newNumberOfPlayers;
        gameObject.SetActive(_enabled);
    }

    private void SetKeyBindForPlayer() // updates the keybind text
    {
        StartCoroutine(MultiplayerLobbyUI.Instance.OnKeyBindSetButtonPressed(PlayerID));
    }

}
