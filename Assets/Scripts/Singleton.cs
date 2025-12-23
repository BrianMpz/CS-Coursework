using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    [SerializeField] private bool dontDestroyOnLoad;

    private protected virtual void OnEnable()
    {
        Instance = this as T;
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }
}
