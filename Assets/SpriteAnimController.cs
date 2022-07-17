using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpriteAnimController : MonoBehaviour
{
    Animator  anim;
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayIdle(Vector2 move)
    {
        anim.Play("RobotIdle");
    }

    public void PlayJump(Vector2 move)
    {
        anim.Play("RobotJump");
    }

    public void PlayWalk(Vector2 move)
    {
        anim.Play("RobotWalk");
    }
}
