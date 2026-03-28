using TMPro;
using UnityEngine;
using UnityEngine.UI;

// manages the logic for adverts that play after a player dies in singleplayer mode
public class AdvertUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CountdownText; // the countdown text
    [SerializeField] private Button SkipButton; // the button to skip the advert
    [SerializeField] private TMP_Text SkipButtonText; // the text for the skip button
    [SerializeField] private Canvas Canvas; // the UI canvas
    private float endTime; // the time that the cooldown ends
    private float enableSkipTime; // the time that the player can skip

    private void Start()
    {
        GameManager.Instance.OnSinglePlayerGameEnd += Show; // show when singleplayer game ends
        SkipButton.onClick.AddListener(Hide); // hide when skipped
        Hide(); // hide and appear when necessary
    }

    private void Show()
    {
        bool _areAdsRemoved = InAppPurchaseUI.AreAdsRemoved(); // cache 

        if (_areAdsRemoved) return; // only show if player has removed ads

        Canvas.enabled = true; // make the UI appear
        SkipButton.enabled = false; // cant skip initially

        endTime = Time.time + 30f; // ad ends 30s ahead
        enableSkipTime = Time.time + 5f; // can skip ad after 5s
    }

    private void Hide()
    {
        Canvas.enabled = false; // make the UI disappear
    }

    private void Update()
    {
        float countDown = endTime - Time.time; // calculate the countdown value
        CountdownText.text = $"{countDown:f1}s"; // update ad countdown text

        float skipTime = enableSkipTime - Time.time; // calculate the skip countdown value
        SkipButtonText.text = $"Skip Advert in {skipTime:f0}s..."; // update skip ad countdown text

        if (countDown <= 0f) Hide(); // hide if countdown is over
        if (skipTime <= 0f) // allow skip advert
        {
            SkipButton.enabled = true; // enable the button for skipping
            SkipButtonText.text = $"Skip Advert!"; // relay to the player that they can skip
        }
    }
}
