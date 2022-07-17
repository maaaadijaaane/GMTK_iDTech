using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInteractObject : MonoBehaviour
{
    public UnityEvent<GameObject> onClicked;

    public void ClickInteract(GameObject player)
    {
        onClicked?.Invoke(player);
    }
}
