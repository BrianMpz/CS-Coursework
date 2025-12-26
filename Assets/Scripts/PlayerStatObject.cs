using UnityEngine;
using UnityEngine.UI;

public class PlayerStatObject : MonoBehaviour
{
    [SerializeField] private GameObject playerModel; // the model of the player
    [SerializeField] private Image bar; // the fill bar

    public void Initialize(int _wins, Color _color)
    {
        SetColor(_color);
        SetWinBar(_wins);
    }

    private void SetColor(Color _color)
    {
        bar.color = _color;
        Image[] playerImages = playerModel.GetComponentsInChildren<Image>();
        foreach (Image image in playerImages)
        {
            image.color = _color;
        }
    }

    private void SetWinBar(int wins)
    {
        bar.fillAmount = (float)wins / MultiplayerSessionManager.WIN_THRESHOLD;
    }
}
