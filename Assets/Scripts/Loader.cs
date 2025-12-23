// This class will aid in switching between scenes
// Takes in an index or string and loads that scene
using UnityEngine.SceneManagement;

public static class Loader
{
    // the current scene that is loaded
    public static Scene currentScene;

    // load the scene using index
    public static void LoadScene(Scene _sceneToLoad)
    {
        int _sceneIndex = (int)_sceneToLoad; //  convert enum to int

        SceneManager.LoadScene(_sceneIndex); // load the scene using index

        // set the new currentScene after the load
        currentScene = _sceneToLoad;
    }

    public static bool IsSceneLoaded(Scene _scene)
    {
        return currentScene == _scene;
    }
}

// list of all of the scenes in the game.
public enum Scene
{
    MainMenu,
    MultiplayerLobby,
    GameScene
}