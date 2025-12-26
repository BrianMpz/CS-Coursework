using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNumPlayersButton : MonoBehaviour
{
    [SerializeField] private int numPlayers; // the number of players to set when the button is pressed
    [SerializeField] private Button button; // the button to set the number of players
    [SerializeField] private TMP_Text numPlayersText; // the number of players to set when the button is pressed
    [SerializeField] private Image image; // the image of the button
    private Color enabledColor = Color.green;
    private Color disabledColor = Color.white;

    void Start()
    {
        MultiplayerLobbyUI.Instance.OnNumberOfPlayersChanged += OnNumberOfPlayersChanged;
        button.onClick.AddListener(OnButtonPressed);
        numPlayersText.text = numPlayers.ToString();
        image.color = disabledColor;
    }

    private void OnButtonPressed()
    {
        MultiplayerLobbyUI.Instance.SetNumberOfPlayersPlaying(numPlayers); // set the number of players in the lobby UI
    }

    private void OnNumberOfPlayersChanged(int _newNumberOfPlayers)
    {
        image.color = numPlayers == _newNumberOfPlayers ? enabledColor : disabledColor;
    }
}
