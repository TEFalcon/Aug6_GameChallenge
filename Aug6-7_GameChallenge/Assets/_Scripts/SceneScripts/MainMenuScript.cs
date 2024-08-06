using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    //Varriables:
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            SceneLoader.LoadNextScene(SceneLoader.Scene.GameScene);
            Debug.Log("Play");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
