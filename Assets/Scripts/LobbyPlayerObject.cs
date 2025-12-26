using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerObject : MonoBehaviour
{
    [SerializeField] private int playerID; // the ID of the player
    [SerializeField] private GameObject playerModel; // the model of the player
    [SerializeField] private TMP_Text playerKeybind; // the keybind of the player
    [SerializeField] private Button setKeybindButton; // the button of the player

    private void Start()
    {
        setKeybindButton.onClick.AddListener(SetKeyBindForPlayer);
        MultiplayerLobbyUI.Instance.OnKeybindsChanged += OnKeybindsChanged;
        MultiplayerLobbyUI.Instance.OnNumberOfPlayersChanged += OnNumberOfPlayersChanged;
        SetColor();
    }

    private void SetColor() // go though all images in the player model and set their color to the player's color
    {
        Color _color = MultiplayerLobbyUI.Instance.PlayerColors[playerID];
        Image[] playerImages = playerModel.GetComponentsInChildren<Image>();
        foreach (Image image in playerImages)
        {
            image.color = _color;
        }
    }

    private void OnKeybindsChanged() // update the keybind text
    {
        if (!gameObject.activeSelf) return;

        KeyCode currentKeybind = MultiplayerLobbyUI.Instance.GetKeybindForPlayer(playerID);
        playerKeybind.text = $"[{KeybindManager.KeyCodeToString(currentKeybind)}]";
    }

    private void OnNumberOfPlayersChanged(int _newNumberOfPlayers) // enable or disable the player object based on the number of players
    {
        bool enabled = playerID < _newNumberOfPlayers;
        gameObject.SetActive(enabled);
    }

    private void SetKeyBindForPlayer() // updates the keybind text
    {
        StartCoroutine(MultiplayerLobbyUI.Instance.OnKeyBindSetButtonPressed(playerID));
    }

}
