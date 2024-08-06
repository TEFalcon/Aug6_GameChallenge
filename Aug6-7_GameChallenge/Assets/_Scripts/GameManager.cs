using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int score;
    [SerializeField] private ScoreUI scoreUI;
    private void ChangeGameState(GameState state)
    {
        this.gState =state;
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

        score= 0;
    }
    private void Update()
    {
        
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
}
