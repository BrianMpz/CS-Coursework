using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        GameManager.Instance.OnSinglePlayerGameEnd += Show;
        QuitButton.onClick.AddListener(QuitGame);
        PlayAgainButton.onClick.AddListener(PlayAgain);

        Hide();
    }

    private void Show()
    {
        Canvas.enabled = true;

        ScoreText.text = "Score: " + ScoreManager.Instance.GetScore();
        HighScoreText.text = "High Score: " + ScoreManager.Instance.GetHighScore();
    }

    private void Hide()
    {
        Canvas.enabled = false;
    }

    private void QuitGame()
    {
        Loader.LoadScene(Scene.MainMenu);
    }

    private void PlayAgain()
    {
        Loader.LoadScene(Scene.GameScene);
    }

    private void Share()
    {
        // TODO by Client
    }
}

