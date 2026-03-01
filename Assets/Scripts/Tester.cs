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
            print("Loading MainMenu Scene");
            Loader.LoadScene(Scene.MainMenu);
            print("Loaded MainMenu Scene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("Loading MultiplayerLobby Scene");
            Loader.LoadScene(Scene.MultiplayerLobby);
            print("Loaded MultiplayerLobby Scene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("Loading GameScene Scene");
            Loader.LoadScene(Scene.GameScene);
            print("Loaded GameScene Scene");
        }
    }

    void TestDataManager()
    {
        DataManager.SaveInt("HighScore", 100);
        int highScore = DataManager.GetIntFromSave("HighScore");
        print("High Score: " + highScore);
    }
}
