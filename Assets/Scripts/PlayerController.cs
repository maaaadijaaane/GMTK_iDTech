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
    private float speed = 0.4f;
    // Jumping
    public static float jumpSpeed= 1.25f;
    public static bool doubleJump = false;
    public static int numJumps = 0;
    private GameObject groundCheck;
    public static bool onGround;
    
    //private bool facingRight = true;
    void Awake()
    {
        player = this;
        groundCheck = GameObject.Find("GroundCollision"); // get ground so we only consider bottom of cube ground collision
    }

    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
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
        else
        {
            rb.AddForce(new Vector2(movement.x, 0.0f) * speed, ForceMode.VelocityChange);
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
        else if(movement.y == 1 && doubleJump && numJumps < 1)
        {
            numJumps += 1;
            movement = Vector2.up * jumpSpeed;
            player.rb.AddForce(movement, ForceMode.Impulse);
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
}