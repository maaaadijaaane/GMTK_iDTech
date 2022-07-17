using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : Interactable
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void use()
    {
        Debug.Log("bouncy ball clicked");
        anim.Play("bounce");
    }
}
