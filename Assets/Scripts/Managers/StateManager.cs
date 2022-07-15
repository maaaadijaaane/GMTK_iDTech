using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public GameObject findPause;
    public GameObject findControls;
    GameObject pause;
    //PauseController pauseController;
    
    private string sceneName;
    private Scene currScene;
    private Scene prevScene;
    private Scene stateManagerScene;
    private GameObject openCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        stateManagerScene = SceneManager.GetActiveScene();
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
    }

    // Update is called once per frame
    public void OpenScene(string sceneName)
    {    
        prevScene = currScene;    
        SceneManager.LoadScene(sceneName);

        stateManagerScene = SceneManager.GetActiveScene();
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
        Time.timeScale = 1;
    }

    public void OpenStartMenu()
    {
        if(GameObject.Find("PauseController") != null)
        {
            pause = GameObject.Find("PauseController");
            //pauseController = pause.GetComponent<PauseController>();
            //pauseController.Unpause();
        }

        SceneManager.LoadScene("StartUI"); // Put Main Menu scene here

        stateManagerScene = SceneManager.GetActiveScene();
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
    }

    // Switching between Pause/How to play
    public void SwitchCanvas()
    {
        if(!findPause.activeSelf)
        {
            findControls.SetActive(false);
            findPause.SetActive(true);
        }
        else if(findPause.activeSelf)
        {
            findControls.SetActive(true);
            findPause.SetActive(false);
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
