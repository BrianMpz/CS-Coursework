using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdvertUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CountdownText;
    [SerializeField] private Button SkipButton;
    [SerializeField] private TMP_Text SkipButtonText;
    [SerializeField] private Canvas Canvas;
    private float endTime; // the time that the cooldown ends
    private float enableSkipTime; // the time that the player can skip

    private void Start()
    {
        GameManager.Instance.OnSinglePlayerGameEnd += Show; // show when singleplayer game ends
        SkipButton.onClick.AddListener(Hide); // hide when skipped
        Hide();
    }

    private void Show()
    {
        bool areAdsRemoved = InAppPurchaseUI.AreAdsRemoved();
        if (areAdsRemoved) return; // only show if player has removed ads

        Canvas.enabled = true;
        SkipButton.enabled = false; // cant skip initially

        endTime = Time.time + 30f; // ad ends 30s ahead
        enableSkipTime = Time.time + 5f; // can skip ad after 5s
    }

    private void Hide()
    {
        Canvas.enabled = false;
    }

    private void Update()
    {
        float countDown = endTime - Time.time;
        CountdownText.text = $"{countDown:f1}s"; // update ad countdown text

        float skipTime = enableSkipTime - Time.time;
        SkipButtonText.text = $"Skip Advert in {skipTime:f0}s..."; // update skip ad countdown text

        if (countDown <= 0f) Hide(); // hide if countdown is over
        if (skipTime <= 0f) // allow skip advert
        {
            SkipButton.enabled = true;
            SkipButtonText.text = $"Skip Advert!";
        }
    }
}
