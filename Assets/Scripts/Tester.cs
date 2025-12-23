using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Keep this object across scene loads
    }

    // Update is called once per frame
    void Update()
    {
        // TestLoading();
        // TestDataManager();
    }

    void TestLoading()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Loading MainMenu Scene");
            Loader.LoadScene(Scene.MainMenu);
            Debug.Log("Loaded MainMenu Scene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Loading MultiplayerLobby Scene");
            Loader.LoadScene(Scene.MultiplayerLobby);
            Debug.Log("Loaded MultiplayerLobby Scene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Loading GameScene Scene");
            Loader.LoadScene(Scene.GameScene);
            Debug.Log("Loaded GameScene Scene");
        }
    }

    void TestDataManager()
    {
        DataManager.SaveInt("HighScore", 100);
        int highScore = DataManager.GetIntFromSave("HighScore");
        Debug.Log("High Score: " + highScore);
    }
}
