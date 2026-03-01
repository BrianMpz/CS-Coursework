using UnityEngine;
using UnityEngine.UI;

public class PauseUI : Singleton<PauseUI>
{
    public bool IsPaused;
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button UnpauseButton;
    [SerializeField] private Button QuitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuitButton.onClick.AddListener(QuitGame);
        PauseButton.onClick.AddListener(Pause);
        UnpauseButton.onClick.AddListener(Unpause);
        Unpause();
    }

    private void Pause()
    {
        IsPaused = true;
        Canvas.enabled = true;
    }

    private void Unpause()
    {
        IsPaused = false;
        Canvas.enabled = false;
    }

    private void QuitGame()
    {
        Loader.LoadScene(Scene.MainMenu);
    }
}
