using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void use()
    {
        Debug.Log("Quit pressed");
        GameManager.Instance.TriggerEvent(GameState.Quit);
    }
}
