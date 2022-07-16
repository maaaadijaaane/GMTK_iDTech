using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    [SerializeField]
    private Rigidbody rb;
    private static Vector2 movement;
    private float speed = 0.5f;
    // Jumping
    public static float jumpSpeed= 1f;
    private static bool onGround;
    private int LayerGround;
    //private bool facingRight = true;
    void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
        LayerGround = LayerMask.NameToLayer("Ground");
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        //sprite = GetComponent<SpriteRenderer>();
        movement = new Vector2();
    }

    void FixedUpdate()
    {
        if(movement.y == 0)
        {
            rb.AddForce(movement * speed, ForceMode.VelocityChange);
        }
    }

    public static void Move(Vector2 move) // InputAction.CallbackContext context
    {   
        movement = move;

        if(movement.y == 1 && onGround)
        {
            movement = Vector2.up * jumpSpeed;
            player.rb.AddForce(movement, ForceMode.Impulse);
            //animator.SetBool("Jump", true);
        }
        else
        {
            movement = new Vector2(movement.x, 0.0f);
        }

        if(movement.x > 0)
        {
            //animator.SetBool("Run", true);
            //facingRight = true;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.x < 0)
        {
            //animator.SetBool("Run", true);
            //facingRight = false;
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.gameObject.layer == LayerGround)
        {
            onGround = true;
        }
        Debug.Log("Entered collision:" + onGround);
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.collider.gameObject.layer == LayerGround)
        {
            onGround = false;
        } 
        Debug.Log("Exit collision:" + onGround);
    }
}