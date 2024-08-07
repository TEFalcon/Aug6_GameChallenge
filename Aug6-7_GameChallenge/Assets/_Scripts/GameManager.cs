using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private float waitingToStartTimer = 0.5f;
    private float countDownTimer = 3f;
    private float runningTimer;

    public static GameManager Instance;
    private GameState gState;

    private bool isGamePaused = false;
    public event EventHandler OnGameOverAction;
    public event EventHandler<OnMenuToggleEventArgs> MenuToggleAction;
    public class OnMenuToggleEventArgs : EventArgs
    {
        public bool ToggleDir;
    }
    private int score;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private TextMeshProUGUI countdownTimerUI;
    public event EventHandler OnCountdownStart;


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
        ChangeGameState(GameState.WaitingToStart);
    }

    private void Start()
    {
        runningTimer = 0;
        score = 0;
        isGamePaused = false;
        MusicManager.Instance.ChangeState(false);
        GameInput.Instance.MenuToggleAction += GameInput_MenuToggleAction;
    }

    private void GameInput_MenuToggleAction(object sender, System.EventArgs e)
    {
        ActivateMenuToggleAction();
        Debug.Log(isGamePaused);
    }

    private void Update()
    {
        if (!IsGamePlayin())
        {

            switch (gState)
            {
                case GameState.WaitingToStart:
                    runningTimer += Time.deltaTime;
                    if (runningTimer > waitingToStartTimer)
                    {
                        runningTimer = countDownTimer;
                        countdownTimerUI.gameObject.SetActive(true);
                        ChangeGameState(GameState.CountDown);
                        OnCountdownStart?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case GameState.CountDown:
                    runningTimer -= Time.deltaTime;
                    float temp = (float)Math.Floor(runningTimer);
                    if (temp + "" != countdownTimerUI.text)
                    {
                        //OnCountdownStart?.Invoke(this, EventArgs.Empty);
                        countdownTimerUI.text = "" + temp;
                    }
                    if (runningTimer <= 0f)
                    {
                        MusicManager.Instance.ChangeState(true);
                        runningTimer = 0;
                        countdownTimerUI.gameObject.SetActive(false);
                        ChangeGameState(GameState.GamePlaying);
                    }
                    break;
            }
        }
    }
    private void OnDestroy()
    {
        isGamePaused = false;
        ChangeGameState(GameState.WaitingToStart);
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

    public void SetGameToOver()
    {
        if (IsGamePlayin())
        {
            ChangeGameState(GameState.GameOver);
            OnGameOverAction?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        }
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
                Time.timeScale = 1f;
            }
            else if (!isGamePaused)
            {
                Time.timeScale = 0f;
                MenuToggleAction?.Invoke(this, new OnMenuToggleEventArgs
                {
                    ToggleDir = true,
                });
            }
            isGamePaused = !isGamePaused;
        }
    }
}
