using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Intro,
    Playing,
    Paused,
    GameOver,
    Win
}
[CreateAssetMenu(fileName = "New Global Pause", menuName = "GGJ2021/Systems/Pause System")]
public class PauseSystem : ScriptableObject
{
    public GameState gameState = GameState.Intro;
    public bool value = false; 
}
