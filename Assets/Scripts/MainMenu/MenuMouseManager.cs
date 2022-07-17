using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuMouseManager : MonoBehaviour
{
    public static MenuMouseManager Instance;
    private int LayerDraggable;
    private int LayerGenerate;
    public static MovableBlock block;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.transform.gameObject.GetComponent<Interactable>() != null)
            {
                hit.collider.transform.gameObject.GetComponent<Interactable>().use();
            }


            else
            {
                //Debug.Log(hit.collider.transform.name);
            }
        }
        

    }

}
