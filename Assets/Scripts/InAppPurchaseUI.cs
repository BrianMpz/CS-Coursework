// Manages in-app purchase UI and all of the logic
// Fulfills Criterion 4, 5
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// handles the IAP UI
public class InAppPurchaseUI : Singleton<InAppPurchaseUI>
{
    [SerializeField] private Button BackButton;
    [SerializeField] private Button UnlockButton;
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements

    [SerializeField] private TMP_Text IAPText;

    private void Start()
    {
        BackButton.onClick.AddListener(OnBackButtonPressed);
        UnlockButton.onClick.AddListener(OnUnlockButtonPressed);
        Hide(); // initially hide
    }

    public void Show()
    {
        Canvas.enabled = true; // enable UI
        IAPText.text = "*In-App Purchase: Remove Ads*";
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
        if (AreAdsRemoved()) IAPText.text = "*Ads have already been removed!*";
        else RemoveAds();
    }

    public static bool AreAdsRemoved()
    {
        return DataManager.GetBoolFromSave("hasRemovedAds"); // check if ads have been removed
    }

    private void RemoveAds()
    {
        IAPText.text = "*Ads Removed!*";
        DataManager.SaveBool("hasRemovedAds", true); // remove ads and save change to storage
    }
}