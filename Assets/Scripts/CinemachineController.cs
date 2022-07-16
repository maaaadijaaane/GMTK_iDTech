using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

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
    public void OnClick()
    {
        zoomOutCamera.enabled = true;
        

        //Temporary code until I can get the MouseManager's block target.
        RaycastHit hit;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            zoomOutCamera.Follow = hit.collider.transform;
        }
        //End Temporary Code Block

        //On Release, camera should return to main character.
    }
}
