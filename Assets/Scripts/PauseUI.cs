using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance;
    public static GameObject pauseScreen; // Need to add through Unity editor
    private static float pausedTimeScale;
    public static bool isPaused = false;
    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(Instance);
        pauseScreen = GameObject.FindGameObjectsWithTag("Pause")[0];
        pauseScreen.SetActive(false);
    }
    public void ContinueGame()
    {
        Debug.Log("Continue");
        Unpause();
    }
    public static void PauseGame()
    {
        Debug.Log("Pause");
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; 
    }
    public static void Unpause()
    {
        Debug.Log("Unpause");
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Set time back to normal
    }
    public void ControlsScreen()
    {
        Debug.Log("Controls");
        StateManager.Instance.SwitchCanvas();
    }
    public void ExitGame()
    {
        GameManager.Instance.TriggerEvent(GameState.Quit);
    }
}
