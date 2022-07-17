using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState State;
    public AudioManager audioManager;
    public static event Action<GameState> OnGameStateChanged;
    public UnityEvent<int> generateRandomCheckpoint;
    public int currCheckpoint;
    public List<int> checkpointNums = new List<int>();
    private Ability levelAbility;
    public UnityEvent<int> onGenerateNewLevel;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Awake()
    {
        if(Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    void Start()
    {
        //UpdateGameState(GameState.MenuScreen);
    }
    
    public void TriggerEvent(GameState newState)
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
                Instance.RandomizeCheckpoint(4);
                break;
            case GameState.Restart:
                StateManager.Instance.RestartScene();
                break;
            case GameState.Building:
                BuildingTower();
                break;
            case GameState.Generate:
                GenerateBlock();
                break;
            case GameState.Quit:
                Application.Quit();
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
        MouseManager.Instance.SetDragging(movingBlock);

        //Enable and disable the grid
        BlockGrid.currentGrid.gameObject.SetActive(true);
        movingBlock.onBlockDropped.AddListener(() => BlockGrid.currentGrid.gameObject.SetActive(false));

        await Task.Yield();
        Instance.TriggerEvent(GameState.Building);
    }
    public void RandomizeCheckpoint(int range)
    {
        string levelName = "TemplateScene";
        float checkPoint = UnityEngine.Random.Range(1, range);
        currCheckpoint = (int) checkPoint;
        /*
        while(checkpointNums.Contains(currCheckpoint))
        {
            checkPoint = UnityEngine.Random.Range(1, range);
            currCheckpoint = (int) checkPoint;
        }
        */
        checkpointNums.Add(currCheckpoint);
        if(currCheckpoint == 1 )
        {
            levelAbility = Ability.Static;
            levelName = "Static";
        }
        else if(currCheckpoint == 2)
        {
            levelAbility = Ability.Sticky;
            levelName = "Sticky";
        }
        if(currCheckpoint == 3)
        {
            levelAbility = Ability.Jump;
            levelName = "Jump";
        }
        if(currCheckpoint == 4)
        {
            levelAbility = Ability.Ladder;
            levelName = "Ladder";
        }

        //checkpoints.Add(levelAbility);
        Debug.Log("Num: " + currCheckpoint);
        StateManager.Instance.OpenScene(levelName); // Add currentLevel here
        onGenerateNewLevel?.Invoke(currCheckpoint);
    }
}

public enum GameState{
    MenuScreen,
    PauseScreen,
    ControlsScreen,
    Play,
    Restart,
    Generate,
    Building,
    Quit
}
