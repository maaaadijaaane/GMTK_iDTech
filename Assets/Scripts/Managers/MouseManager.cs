using System.Collections;
using System.Collections.Generic;
using TempustScript;
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
    public UnityEvent onDragStop;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LayerDraggable = LayerMask.NameToLayer("Draggable");
        LayerGenerate = LayerMask.NameToLayer("Generate");
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        RaycastHit hit;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray,out hit,100.0f))
        {
            if (hit.collider != null && hit.collider.tag == "Click Interact")
            {
                MouseInteractObject obj = hit.collider.GetComponent<MouseInteractObject>();
                obj.ClickInteract(PlayerController.player.gameObject);
            }
        }

        if (TextboxController.singleton.isOpen)
        {
            TextboxController.singleton.Continue();
        }
    }

    public void OnDragClick()
    {
        if(dragging == false)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
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
                    GameManager.Instance.TriggerEvent(GameState.Generate);
                    totalBlocksAllowed -= 1;
                }
            }
        }
    }

    public void SetDragging(MovableBlock block)
    {
        dragging = true;
        onDragStart?.Invoke(block.gameObject);
        block.onBlockDropped.AddListener(() => onDragStop?.Invoke());
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