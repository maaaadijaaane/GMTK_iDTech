using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdatePlayer : MonoBehaviour
{
    public Ability ability = Ability.None;
    public static Ability activeAbility = Ability.None; // Set activeAbility when player chooses to use the Static, Sticky, or Ladder abilities, 
                                                        // turn off until they choose block from factory for ability
    public static bool climbing = false;
    private GameObject parentSticky = null;
    private GameObject childSticky = null;
    private int LayerGround;
    void Start()
    {
        LayerGround = LayerMask.NameToLayer("Ground");
        activeAbility = ability;
    }
    public void OnClick()
    {
        if(activeAbility == Ability.Sticky)
        {
            RaycastHit hit; 
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Vector3 mouse = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 10f;
            if (Physics.Raycast(ray,out hit,100.0f) && hit.collider.gameObject.layer == LayerGround) 
            {
                if(Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    if (parentSticky != null && childSticky == null)
                    {
                        Debug.Log("Sticky Object #2");
                        MovableBlock script = hit.collider.gameObject.GetComponent<MovableBlock>();
                        hit.collider.gameObject.AddComponent<FixedJoint>();
                        hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody=parentSticky.gameObject.GetComponent<Rigidbody>();
                        activeAbility = Ability.None;
                        parentSticky = null;
                    }
                    else if(parentSticky == null)
                    {
                        parentSticky = hit.collider.gameObject;
                        Debug.Log("Sticky Object #1");
                    }
                }
            }
        }

    }
    public void OnMove(InputAction.CallbackContext context) // InputAction.CallbackContext context
    {  
        Vector2 movementVector = context.ReadValue<Vector2>();
        PlayerController.Move(movementVector);
    }
}
