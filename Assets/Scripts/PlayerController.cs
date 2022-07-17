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
    public static float jumpSpeed= 10.1f;
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
        }
        rb.AddForce(movement.x * Vector3.right * speed, ForceMode.VelocityChange);
    }

    public void Move(Vector2 move) // InputAction.CallbackContext context
    {
        movement = move;
        if(movement.y == 1 && UpdatePlayer.climbing)
        {
            Debug.Log("Climbing");
            movement.y = jumpSpeed;
        }
        else if(movement.y == 1 && onGround)
        {
            movement.y = jumpSpeed;
            //animator.SetBool("Jump", true);
        }
        else if(movement.y == 1 && doubleJump && numJumps < 1)
        {
            numJumps += 1;
            movement.y = jumpSpeed;
        }
        else
        {
            movement.y = 0;
        }

        movement.x = move.x;

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