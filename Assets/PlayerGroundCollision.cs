using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{
    private int LayerGround;
    // Start is called before the first frame update
    void Start()
    {
        LayerGround = LayerMask.NameToLayer("Ground");
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.layer == LayerGround)
        {
            PlayerController.onGround = true;
            PlayerController.numJumps = 0;
        }
    }
    void OnTriggerExit(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.layer == LayerGround)
        {
            PlayerController.onGround = false;
        } 
    }
}
