using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnGameOverAction += GameManager_OnGameOverAction;
        ;
        gameObject.SetActive(false);
    }

    private void GameManager_OnGameOverAction(object sender, System.EventArgs e)
    {

            gameObject.SetActive(true);
        
    }


    public void SendToMainMenuScene()
    {
        SceneLoader.LoadNextScene(SceneLoader.Scene.MainMenuScene);
    }
    public void ResetScene()
    {
        SceneLoader.LoadNextScene(SceneLoader.Scene.GameScene);
    }
}
