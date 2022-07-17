using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdatePlayer : MonoBehaviour
{
    public Ability ability = Ability.None;
    public static Ability activeAbility = Ability.None; // Set activeAbility when player chooses to use the Static, Sticky, or Ladder abilities, 
                                                        // turn off until they choose block from factory for ability
    void Start()
    {
        activeAbility = ability;
    }
    public void OnMove(InputAction.CallbackContext context) // InputAction.CallbackContext context
    {  
        Vector2 movementVector = context.ReadValue<Vector2>();
        PlayerController.Move(movementVector);
    }
}
