using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        WaitingToStart,
        CountDown,
        GamePlaying,
        GameOver
    }
    public static GameManager Instance;
    private GameState gState;

    private bool isGamePaused = false;
    public event EventHandler<OnMenuToggleEventArgs> MenuToggleAction;
    public class OnMenuToggleEventArgs : EventArgs
    {
        public bool ToggleDir;
    }
    private int score;
    [SerializeField] private ScoreUI scoreUI;
    private void ChangeGameState(GameState state)
    {
        this.gState = state;
    }

    private float timerCountDown;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        gState = new GameState();
    }

    private void Start()
    {
        //for now:
        ChangeGameState(GameState.GamePlaying);
        //del

        score = 0;
        isGamePaused = false;
        GameInput.Instance.MenuToggleAction += GameInput_MenuToggleAction;
    }

    private void GameInput_MenuToggleAction(object sender, System.EventArgs e)
    {
        ActivateMenuToggleAction();
        Debug.Log(isGamePaused);
    }

    private void Update()
    {
        switch (gState)
        {
            case GameState.WaitingToStart:

                break;
        }
    }
    private void OnDestroy()
    {
        isGamePaused= false;
    }
    public void AddOneToScore()
    {
        score++;
        scoreUI.ChageScoreUI(score);
    }

    public GameState GetGameState() { return gState; }


    public bool IsGamePlayin()
    {
        return gState == GameState.GamePlaying;
    }


    public void ActivateMenuToggleAction()
    {
        if (IsGamePlayin())
        {

            if (isGamePaused)
            {
                MenuToggleAction?.Invoke(this, new OnMenuToggleEventArgs
                {
                    ToggleDir = false,
                });
            }
            else if (!isGamePaused)
            {
                MenuToggleAction?.Invoke(this, new OnMenuToggleEventArgs
                {
                    ToggleDir = true,
                });
            }
            isGamePaused = !isGamePaused;
        }
    }
}
