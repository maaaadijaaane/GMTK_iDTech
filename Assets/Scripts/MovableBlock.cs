using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovableBlock : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Moving()
    {
        if(MouseManager.dragging == true)
        {
            Debug.Log("Entered Moving object");
            // Create event to do this on object instead
            if(MouseManager.dragging == true)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue(); 
                mousePos.z = 10f;
                Vector3 mouse = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = new Vector3(mouse.x, mouse.y, 0.0f);
                if(Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    this.Dropped();
                    MouseManager.dragging = false;
                }
            }
        }
    }
    public void Dropped()
    {
        rb.isKinematic = false;
        MouseManager.dragging = false;
    }
}
