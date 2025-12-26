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
        GameManager.Instance.OnSinglePlayerGameEnd += Show;
        SkipButton.onClick.AddListener(Hide);
        Hide();
    }

    private void Show()
    {
        bool areAdsRemoved = InAppPurchaseUI.AreAdsRemoved();

        if (areAdsRemoved) return;

        Canvas.enabled = true;
        SkipButton.enabled = false;

        endTime = Time.time + 30f;
        enableSkipTime = Time.time + 5f;
    }

    private void Hide()
    {
        Canvas.enabled = false;
    }

    private void Update()
    {
        float countDown = endTime - Time.time;
        CountdownText.text = $"{countDown:f1}s";

        float skipTime = enableSkipTime - Time.time;
        SkipButtonText.text = $"Skip Advert in {skipTime:f0}s...";

        if (countDown <= 0f) Hide();
        if (skipTime <= 0f)
        {
            SkipButton.enabled = true;
            SkipButtonText.text = $"Skip Advert!";
        }
    }
}
