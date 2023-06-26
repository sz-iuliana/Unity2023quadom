using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }



    public event EventHandler OnStateChanged;
     public event EventHandler OnGamePaused;
     public event EventHandler OnGameUnpaused;

   

    private enum State
    {

        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer ;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePaused=false;


    private void Start()
    {
        GameInput.Instance.OnPauseAction += GamePaused;
      
    }

    public void GamePaused(object sender , EventArgs e)
    {
        TogglePause();


    }
    private void Awake()
    { Instance = this; 
        state = State.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer <= 0f)
                {
                    state=State.CountdownToStart;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;

                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;


        }

        Debug.Log(state);
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1-( gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePause()
    {

        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }

    }
}