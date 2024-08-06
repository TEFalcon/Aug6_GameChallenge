using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.MenuToggleAction += GameManager_MenuToggleAction;
        Hide();
    }

    private void GameManager_MenuToggleAction(object sender, GameManager.OnMenuToggleEventArgs e)
    {
        if (GameManager.Instance.IsGamePlayin())
        {
            if (e.ToggleDir)
            {
                Show();
            }
            else
            {
                Hide();

            }
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);

        //ResumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
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
