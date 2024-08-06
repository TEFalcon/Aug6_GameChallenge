
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;


    public static void LoadNextScene(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;


        SceneManager.LoadScene((int)Scene.LoadingScene);
    }


    public static void LoaderCallback()
    {
        SceneManager.LoadScene((int)(targetScene));
        Time.timeScale = 1f;
    }
}
