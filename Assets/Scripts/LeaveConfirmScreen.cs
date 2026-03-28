using UnityEngine;
using UnityEngine.UI;

// alloes players to confirm wether they actually want to leave or not
public class LeaveConfirmScreen : Singleton<LeaveConfirmScreen>
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Button YesButton;
    [SerializeField] private Button NoButton;

    private void Start()
    {
        YesButton.onClick.AddListener(OnYesButtonPressed);
        NoButton.onClick.AddListener(OnNoButtonPressed);
        Hide();
    }

    public void Show() => Canvas.enabled = true;

    private void Hide() => Canvas.enabled = false;

    private void OnYesButtonPressed() => Application.Quit();

    private void OnNoButtonPressed() => Hide();

}
