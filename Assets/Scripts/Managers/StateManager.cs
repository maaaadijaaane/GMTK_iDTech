using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    
    private string sceneName;
    private Scene currScene;
    private Scene prevScene;
    private GameObject openCanvas;

    // Subscribe different scenes/UI to different events
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Awake()
    {
        Instance = this;
        //GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        DontDestroyOnLoad(gameObject); 
    }

    void OnDestroy()
    {
        //GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if(state == GameState.MenuScreen)
        {
            //OpenScene("MainMenu.tscn");
        }
        if(state == GameState.PauseScreen || state == GameState.ControlsScreen)
        {
            SwitchCanvas();
        }
        if(state == GameState.Play)
        {
            //OpenScene("LevelName.tscn");
        }
        if(state == GameState.Building)
        {
            Debug.Log("Entered GameManagerOnGameStateChanged -> Building");
            MouseManager.block.Moving();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
    }

    // Update is called once per frame
    public void OpenScene(string sceneName)
    {    
        prevScene = currScene;    
        SceneManager.LoadScene(sceneName);

        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
        Time.timeScale = 1;
    }

    public void OpenStartMenu()
    {
        if(GameObject.Find("PauseController") != null)
        {
            //pause = GameObject.Find("PauseController");
            //pauseController = pause.GetComponent<PauseController>();
            //pauseController.Unpause();
        }

        //SceneManager.LoadScene("StartUI"); // Put Main Menu scene here

        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
    }

    // Switching between Pause/How to play
    public void SwitchCanvas()
    {
        if(PauseUI.isPaused)
        {
            Debug.Log("Set active to true");
            //findControls.SetActive(false);
            PauseUI.pauseScreen.SetActive(true);
            PauseUI.PauseGame();
        }
        else if(!PauseUI.isPaused)
        {
            Debug.Log("Set active to false");
            //findControls.SetActive(true);
            PauseUI.pauseScreen.SetActive(false);
            PauseUI.Unpause();
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
