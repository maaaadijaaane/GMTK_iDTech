using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState State;
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
                Debug.Log("GameManager");
                StateManager.Instance.SwitchCanvas();
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
            case GameState.Generate:
                GenerateBlock();
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
    static async void GenerateBlock()
    {
        MouseManager.generateBlock.AddBlock();
        //MovableBlock movingBlock = MouseManager.block;
        MovableBlock movingBlock = BlockFactory.blocks[BlockFactory.blocks.Count - 1].GetComponent<MovableBlock>();
        MouseManager.block = movingBlock;

        //Enable and disable the grid
        BlockGrid.currentGrid.gameObject.SetActive(true);
        movingBlock.onBlockDropped.AddListener(() => BlockGrid.currentGrid.gameObject.SetActive(false));

        await Task.Yield();
        TriggerEvent(GameState.Building);
    }
}

public enum GameState{
    MenuScreen,
    PauseScreen,
    ControlsScreen,
    Play,
    Generate,
    Building
}
