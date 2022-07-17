using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static GameObject pauseScreen; // Need to add through Unity editor
    private static float pausedTimeScale;
    public static bool isPaused = false;
    void Awake()
    {
        pauseScreen = GameObject.FindGameObjectsWithTag("Pause")[0];
        pauseScreen.SetActive(false);
    }
    public void ContinueGame()
    {
        Unpause();
    }
    public static void PauseGame()
    {
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; 
    }
    public static void Unpause()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Set time back to normal
    }
}