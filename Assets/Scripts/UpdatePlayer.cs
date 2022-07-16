using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdatePlayer : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext context) // InputAction.CallbackContext context
    {  
        Vector2 movementVector = context.ReadValue<Vector2>();
        PlayerController.Move(movementVector);
    }
}
