// Manages all of the UI in the Main Menu screen
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MainMenuUI : Singleton<MainMenuUI>
{
    [SerializeField] private Button EndlessModeButton;
    [SerializeField] private Button MultiplayButton;
    [SerializeField] private Button ShopButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements

    private void Start()
    {
        EndlessModeButton.onClick.AddListener(OnEndlessModeButtonPressed);
        MultiplayButton.onClick.AddListener(OnMultiplayButtonPressed);
        ShopButton.onClick.AddListener(OnShopButtonPressed);
        QuitButton.onClick.AddListener(OnQuitButtonPressed);

        Show();
    }

    private void Show()
    {
        Canvas.enabled = true; // enable UI
    }

    private void Hide()
    {
        Canvas.enabled = false; // disable UI
    }

    private void OnEndlessModeButtonPressed()
    {
        Loader.LoadScene(Scene.GameScene); // load game scene
    }

    private void OnMultiplayButtonPressed()
    {
        Loader.LoadScene(Scene.MultiplayerLobby); // load multiplayer lobby
    }

    private void OnShopButtonPressed()
    {
        InAppPurchaseUI.Instance.Show(); // open the IAP UI
    }

    private void OnQuitButtonPressed()
    {
        Application.Quit(); // leave the game
    }
}