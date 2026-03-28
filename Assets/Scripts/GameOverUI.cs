using TMPro;
using UnityEngine;
using UnityEngine.UI;

// singleplayer game ovcer screen
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button PlayAgainButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnSinglePlayerGameEnd += Show; // appear when the singleplayer game ends
        QuitButton.onClick.AddListener(QuitGame); // quit the game when the quit button is pressed
        PlayAgainButton.onClick.AddListener(PlayAgain); // play again once the play again button is pressed

        Hide(); // initially hide
    }

    private void Show()
    {
        Canvas.enabled = true;

        ScoreText.text = "Score: " + ScoreManager.Instance.GetScore(); // set to the score that the player got
        HighScoreText.text = "High Score: " + ScoreManager.Instance.GetHighScore(); // set to the current hight score
    }

    private void Hide() => Canvas.enabled = false;

    private void QuitGame() => Loader.LoadScene(Scene.MainMenu);

    private void PlayAgain() => Loader.LoadScene(Scene.GameScene);

    private void Share()
    {
        // TODO by Client
    }
}

