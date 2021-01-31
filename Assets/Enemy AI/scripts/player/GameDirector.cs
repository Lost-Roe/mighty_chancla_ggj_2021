using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public PauseSystem stateManager;
    public GameState prevStatate;
    public int currentJellies;
    public Transform[] jellyPoints;

    private void Start()
    {
        stateManager.gameState = GameState.Intro;
        currentJellies = 0;
    }

    public Transform GetJellyTarget()
    {
        return jellyPoints[currentJellies]; 
    }

    void Update()
    {
      switch(stateManager.gameState)
        {
            case GameState.Intro:
                stateManager.gameState = GameState.Playing;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.Playing:
                if(Time.timeScale != 1)
                    Time.timeScale = 1;
                break;
            case GameState.GameOver:
                if (Time.timeScale != 1)
                    Time.timeScale = 1;
                break;
            case GameState.Win:
                if (Time.timeScale != 1)
                    Time.timeScale = 1;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (stateManager.gameState != GameState.Paused)
            {
                Debug.Log("Game Paused!");
                prevStatate = stateManager.gameState;
                stateManager.gameState = GameState.Paused;
            } else
            {
                Debug.Log("Game Playing!");
                stateManager.gameState = prevStatate;
            } 
        }
    }
}
