using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClickAction;
    [SerializeField]
    private float playerSpeed = 10f;
    private Vector3 targetPosition;
    private Coroutine coroutine;
    private int LayerGround;
    void Awake()
    {
        LayerGround = LayerMask.NameToLayer("Ground");
    }
    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.started += Move;
    }
    private void OnDisable()
    {
        mouseClickAction.Disable();
        mouseClickAction.canceled -= Move;
    }
    private void Move(InputAction.CallbackContext context) // InputAction.CallbackContext context
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, hitInfo: out RaycastHit hit) && hit.collider)
        {
            if(coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
            targetPosition = hit.point;
        }
    }
    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 destination = Vector3.MoveTowards(transform.position,target, playerSpeed * Time.deltaTime);
            transform.position = destination;
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 1);
    }
}