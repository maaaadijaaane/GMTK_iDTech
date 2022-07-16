using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public int totalBlocksAllowed = 5;
    public static BlockFactory generateBlock;
    public static MouseManager Instance;
    public static bool dragging = false;
    private int LayerDraggable;
    private int LayerGenerate;
    public static MovableBlock block;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        LayerDraggable = LayerMask.NameToLayer("Draggable");
        LayerGenerate = LayerMask.NameToLayer("Generate");
    }

    public void OnClick()
    {

    }

    public void OnDragClick()
    {
        if(dragging == false)
        {
            RaycastHit hit; 
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            Ray ray = Camera.main.ScreenPointToRay(mousePos); 

            if (Physics.Raycast (ray,out hit,100.0f)) 
            {
                if(hit.transform.gameObject.layer == LayerGenerate)
                {
                    dragging = true;
                    generateBlock = hit.collider.GetComponent<BlockFactory>();
                    GameManager.TriggerEvent(GameState.Generate);
                    totalBlocksAllowed -= 1;
                }
            }
        }
    }

    public void OnRotate()
    {
        if(block != null)
        {
            Vector3 rotate = new Vector3(0,0,90);
            block.transform.Rotate(rotate);
        }
    }

    public void OnPause()
    {
        GameManager.TriggerEvent(GameState.PauseScreen);
    }
}
