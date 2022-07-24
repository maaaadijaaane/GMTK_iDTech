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
    private Animator animator;
    public float speed = 0.7f;
    public float maxSpeed = 3f;
    public float groundCheckDist = .1f;
    // Jumping
    public float jumpSpeed= 3f;
    public static bool doubleJump = false;
    public static int numJumps = 0;
    public static bool onGround;
    CapsuleCollider capsule;

    //private bool facingRight = true;
    void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        //sprite = GetComponent<SpriteRenderer>();
        movement = new Vector2();
        capsule = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        float velocityY = rb.velocity.y;
        if(movement.y > 0) // Moving Horizontal
        {
            velocityY = jumpSpeed;
            movement.y = 0;
            //rb.AddForce(movement * speed, ForceMode.VelocityChange);
        }
        rb.AddForce(movement.x * Vector3.right * speed, ForceMode.VelocityChange);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), velocityY);

        RaycastHit hit;
        bool hasGround = Physics.SphereCast(transform.position + ((0.2f + capsule.radius) * Vector3.up), capsule.radius * .99f, Vector3.down, out hit, 0.2f + groundCheckDist + capsule.radius, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore);
        //Debug.Log(hit.normal.y);
        if (hit.normal.y < 0.1)
        {
            hasGround = false;
        }

        Debug.Log(hit.collider);
        //Debug.DrawRay(transform.position + 0.2f * Vector3.up, Vector3.down * (0.2f + groundCheckDist));

        if (hasGround)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        animator.SetBool("Grounded", onGround);
        animator.SetFloat("Walk", Mathf.Abs(rb.velocity.x));
    }

    public void Move(Vector2 move) // InputAction.CallbackContext context
    {
        //rb.velocity = new Vector3(move.x, move.y * jumpSpeed, 0.0f);
        movement = move;

        if (movement.y > 0 && UpdatePlayer.climbing)
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
            movement.y = 0;
        }

        if (movement.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        //movement.x = move.x;
        //rb.velocity = new Vector3(movement.x * speed, movement.y * jumpSpeed, 0.0f);
        //movement = move;
    }
}