using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public static ControlsUI Instance;
    public static GameObject controlScreen; // Need to add through Unity editor

    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(Instance);
        controlScreen = GameObject.FindGameObjectsWithTag("Controls")[0];
        controlScreen.SetActive(false);
    }
    public void PauseScreen()
    {
        StateManager.Instance.SwitchCanvas();
    }
    public void ExitGame()
    {
        GameManager.Instance.TriggerEvent(GameState.Quit);
    }
}
