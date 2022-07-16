using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MouseManager))]
public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera zoomOutCamera;
    // Start is called before the first frame update
    void Start()
    {
        zoomOutCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickDrag()
    {
        zoomOutCamera.enabled = true;
        
        if (MouseManager.block != null)
        {
            zoomOutCamera.Follow = MouseManager.block.transform;
        }

        //On Release, camera should return to main character.
    }
}
