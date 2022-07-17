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
    private float maxSpeed = 4f;
    // Jumping
    public static float jumpSpeed= 2f;
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
        if(movement.y == 0) // Moving Horizontal
        {
            rb.AddForce(movement.y * Vector3.up, ForceMode.Impulse);
            //rb.AddForce(movement * speed, ForceMode.VelocityChange);
        }
        rb.AddForce(movement.x * Vector3.right * speed, ForceMode.VelocityChange);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    public void Move(Vector2 move) // InputAction.CallbackContext context
    {
        rb.velocity = new Vector3(move.x, move.y * jumpSpeed, 0.0f);
        movement = move;
        if(movement.y > 0 && UpdatePlayer.climbing)
        {
            Debug.Log("Climbing");
            movement.y = jumpSpeed;
        }
        else if(movement.y > 0 && onGround)
        {
            Debug.Log("Jumping");
            movement.y = jumpSpeed;
            //animator.SetBool("Jump", true);
        }
        else if(movement.y > 0 && doubleJump && numJumps < 1)
        {
            Debug.Log("Double Jumo");
            numJumps += 1;
            movement.y = jumpSpeed;
        }
        else
        {
            Debug.Log("Moving");
            movement.y = 0;
        }

        movement.x = move.x;
        rb.velocity = new Vector3(movement.x * speed, movement.y * jumpSpeed, 0.0f);
        movement = move;
    }
}