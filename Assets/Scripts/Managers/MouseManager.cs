using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    public UnityEvent<GameObject> onDragStart;

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

    public void OnDragClick()
    {
        if(dragging == false)
        {
            RaycastHit hit; 
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = mousePos;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);

            //if (Physics.Raycast(ray,out hit,100.0f)) 
            if (results.Count > 0)
            {
                if(results[0].gameObject.layer == LayerGenerate)
                {
                    generateBlock = results[0].gameObject.GetComponent<BlockFactory>();
                    if (generateBlock == null)
                        generateBlock = results[0].gameObject.GetComponentInParent<BlockFactory>();
                    GameManager.TriggerEvent(GameState.Generate);
                    totalBlocksAllowed -= 1;
                }
            }
        }
    }

    public void SetDragging(MovableBlock block)
    {
        dragging = true;
        onDragStart?.Invoke(block.gameObject);
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if(block != null)
        {
            block.Rotate();
        }
    }
}