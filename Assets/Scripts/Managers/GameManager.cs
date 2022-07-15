using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState State;
    public static StateManager Scene;
    public static event Action<GameState> OnGameStateChanged;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void Start()
    {
        //UpdateGameState(GameState.MenuScreen);
    }
    
    public static void TriggerEvent(GameState newState)
    {
        State = newState;
        switch(newState){
            case GameState.MenuScreen:
                //Scene.OpenScene("MainMenu.tscn");
                break;
            case GameState.PauseScreen:
                //Scene.SwitchCanvas();
                break;
            case GameState.ControlsScreen:
                //Scene.SwitchCanvas();
                break;
            case GameState.Play:
                // Scene.OpenScene("MainMenu.tscn"); // Add currentLevel here
                break;
            case GameState.Building:
                BuildingTower();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    static async void BuildingTower()
    {
        while(MouseManager.dragging == true)
        {
            MouseManager.block.Moving();
            //Debug.Log("Async function");
            await Task.Yield();
        }
    }
}

public enum GameState{
    MenuScreen,
    PauseScreen,
    ControlsScreen,
    Play,
    Building
}
