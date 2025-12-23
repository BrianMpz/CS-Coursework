using UnityEngine;

public class Watermark : Singleton<Watermark>
{
    [SerializeField] private Canvas Canvas; // script that holds and manages UI Elements

    private void Start()
    {
        Show();
    }

    private void Show()
    {
        Canvas.enabled = true; // enable UI
    }
}
