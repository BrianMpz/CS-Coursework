// Manages in-app purchase UI and all of the logic
// Fulfills Criterion 4, 5
using UnityEngine;
using UnityEngine.UI;

public class InAppPurchaseUI : Singleton<InAppPurchaseUI>
{
    [SerializeField] private Button BackButton;
    [SerializeField] private Button UnlockButton;
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements

    private void Start()
    {
        BackButton.onClick.AddListener(OnBackButtonPressed);
        UnlockButton.onClick.AddListener(OnUnlockButtonPressed);

        Hide();
    }

    public void Show()
    {
        Canvas.enabled = true; // enable UI
    }

    private void Hide()
    {
        Canvas.enabled = false; // disable UI
    }

    private void OnBackButtonPressed()
    {
        Hide();
    }

    private void OnUnlockButtonPressed()
    {
        if (AreAdsRemoved()) Debug.Log("Ads have already been removed.");
        else RemoveAds();

        Hide();
    }

    public static bool AreAdsRemoved()
    {
        return DataManager.GetBoolFromSave("hasRemovedAds"); // check if ads have been removed
    }

    private void RemoveAds()
    {
        Debug.Log("Ads Removed!");
        DataManager.SaveBool("hasRemovedAds", true); // remove ads and save change to storage
    }
}