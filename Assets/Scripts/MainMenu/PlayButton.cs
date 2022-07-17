using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : Interactable
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
        //Hit the play button, load the next scene
        //need to insert use of Madi's game manager here 
        Debug.Log("Play pressed");
        GameManager.TriggerEvent(GameState.Play);
    }


}
