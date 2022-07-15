using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static StateManager Scene;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action Building;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MenuScreen);
    }

    void UpdateGameState(GameState newState)
    {
        State = newState;
        switch(newState){
            case GameState.MenuScreen:
                Scene.OpenScene("MainMenu.tscn");
                break;
            case GameState.PauseScreen:
                Scene.SwitchCanvas();
                break;
            case GameState.ControlsScreen:
                Scene.SwitchCanvas();
                break;
            case GameState.Play:
                // Scene.OpenScene("MainMenu.tscn"); // Add currentLevel here
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState{
    MenuScreen,
    PauseScreen,
    ControlsScreen,
    Play
}
