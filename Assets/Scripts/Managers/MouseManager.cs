using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    private bool dragging = false;
    private Transform block;
    private Rigidbody rb;
    private int LayerDraggable;
// Start is called before the first frame update
    void Start()
    {
        LayerDraggable = LayerMask.NameToLayer("Draggable");
    }

    // Update is called once per frame
    void Update()
    {
        // Create event to do this on object instead
        if(dragging == true)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            mousePos.z = 10f;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(mousePos);
            block.position = new Vector3(mouse.x, mouse.y, 0.0f);
            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                this.OnRelease();
                dragging = false;
            }
        }
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
                if(hit.transform.gameObject.layer == LayerDraggable)
                {
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    dragging = true;
                    block = hit.transform;
                    rb = hit.rigidbody;
                }
            }
        }
    }
    public void OnRelease()
    {
        rb.isKinematic = false;
    }

}
