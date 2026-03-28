using UnityEngine;
using UnityEngine.UI;

public class PlayerStatObject : MonoBehaviour
{
    [SerializeField] private GameObject PlayerModel; // the model of the player
    [SerializeField] private Image Bar; // the fill bar

    public void Initialize(int _wins, Color _color)
    {
        SetColor(_color);
        SetWinBar(_wins);
    }

    private void SetColor(Color _color)
    {
        Bar.color = _color;
        Image[] _playerImages = PlayerModel.GetComponentsInChildren<Image>();
        foreach (Image image in _playerImages) // for each body part in the stick figure, set to the player's color
        {
            image.color = _color;
        }
    }

    private void SetWinBar(int _wins)
    {
        Bar.fillAmount = (float)_wins / MultiplayerSessionManager.WIN_THRESHOLD; // show how close the player is to winning
    }
}
