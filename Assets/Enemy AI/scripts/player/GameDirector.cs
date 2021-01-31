using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public PauseSystem stateManager;
    public GameState prevStatate;
    public int currentJellies;
    public Transform[] jellyPoints;

    public GameObject player;

    public float maxDistance;
    public float minDistance;
    public float anxiety;
    public float intuition;

    private float[] distanceChecks;
    private float[] enemyDistanceCheck;

    private List<GameObject> enemyPoints;
    private List<GameObject> interestPoints;

    private void Start()
    {
        stateManager.gameState = GameState.Intro;
        currentJellies = 0;

        CheckForInterestPoints();        
    }

    public Transform GetJellyTarget()
    {
        return jellyPoints[currentJellies]; 
    }

    public void CheckForInterestPoints()
    {
        interestPoints = new List<GameObject>();
        enemyPoints = new List<GameObject>();

        var jellies = GameObject.FindGameObjectsWithTag("Jelly");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject j in jellies)
            interestPoints.Add(j);
        foreach (GameObject e in enemies)
        {
            interestPoints.Add(e);
            enemyPoints.Add(e);
        }

        enemyDistanceCheck = new float[enemyPoints.Count];
        distanceChecks = new float[interestPoints.Count];
    }

    public void CheckDistances()
    {
        for(int g = 0; g < interestPoints.Count; g++)
        {
            distanceChecks[g] = Vector3.Distance(player.transform.position, interestPoints[g].transform.position);
        }

        for (int e = 0; e < enemyPoints.Count; e++)
        {
            enemyDistanceCheck[e] = Vector3.Distance(player.transform.position, enemyPoints[e].transform.position);
        }

        float closestInterestPoint = distanceChecks.Min();
        float anxietyCheck = Mathf.Clamp(closestInterestPoint, minDistance, maxDistance);
        float anxietyConvertor = anxietyCheck / maxDistance;

        anxiety = 1 - anxietyConvertor;

        float closestEnemy = enemyDistanceCheck.Min();
        float enemyCheck = Mathf.Clamp(closestEnemy, minDistance, maxDistance);
        float enemyConvertor = enemyCheck / maxDistance;

        intuition = 1 - enemyConvertor;
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
                CheckDistances();
                break;
            case GameState.GameOver:
                FindObjectOfType<UiManager>().Activate(UiName.GameOver);
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
