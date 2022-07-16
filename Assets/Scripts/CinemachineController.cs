using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MouseManager))]
public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera zoomOutCamera;
    private CinemachineTargetGroup playerGroup;

    // Start is called before the first frame update
    void Start()
    {
        if(zoomOutCamera == null)
        {
            zoomOutCamera = GameObject.Find("ZoomOutCam").GetComponent<CinemachineVirtualCamera>();
            playerGroup = zoomOutCamera.Follow.transform.GetComponent<CinemachineTargetGroup>();
            zoomOutCamera.enabled = false;

            Debug.LogWarning("A cinemachine camera was not assigned. The first Virtual Camera named ZoomOutCam has been used instead. " + zoomOutCamera.name);
        }
    }

    public void DragEnd()
    {
        zoomOutCamera.enabled = false;
        playerGroup.m_Targets[1].target = null;
        MouseManager.block.onBlockDropped.RemoveListener(DragEnd);
    }

    public void OnDragClick()
    {
        
        if (MouseManager.block != null)
        {
            zoomOutCamera.enabled = true;
            playerGroup.m_Targets[1].target = MouseManager.block.transform;
            MouseManager.block.onBlockDropped.AddListener(DragEnd);
        }

        //On Release, camera should return to main character.
    }
}
