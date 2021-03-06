using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : Interactable
{
    //how hard the dice are being rolled
    public float rollForce;
    public float rollTorque;
    //outcome of roll
    public int rollResult;

    private Rigidbody rb;

    private Transform side1;
    private Transform side2;
    private Transform side3;
    private Transform side4;
    private Transform side5;
    private Transform side6;

    public bool rolling = false;
    //how long to wait before checking for result, since it starts touching the ground
    private float rollCheckWait = 0.3f;
    private float clock = 0;

    public UnityEvent<int> onDiceFinishRoll;

    public AudioClip diceHit;
    public AudioClip diceGround;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            if (clock >= rollCheckWait)
            {
                if (rb.velocity.x ==0 && rb.velocity.y == 0 && rb.velocity.z == 0)
                {
                    checkLanded();
                }
            }
            else
            {
                clock += Time.deltaTime;
            }
        }
    }

    public override void use()
    {
        roll();
    }

    public void roll()
    {
        rb.AddForce(0, rollForce, 0, ForceMode.Impulse);
        rb.AddTorque(Random.Range(-rollTorque*2, rollTorque * 2), 0, Random.Range(-rollTorque * 2, rollTorque * 2));
        rolling = true;
    }

    private void checkLanded()
    {
        /**
        RaycastHit hit;
        if(Physics.Raycast(side1.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 6;
        }
        if(Physics.Raycast(side2.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 5;
        }
        if(Physics.Raycast(side3.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 1;
        }
        if(Physics.Raycast(side4.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 6;
        }
        if(Physics.Raycast(side5.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 2;
        }
        if(Physics.Raycast(side6.position, Vector3.up, out hit, 0.01f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            rollResult = 4;
        }
        **/
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.21f))
        {
            rolling = false;
            Debug.Log(hit.collider.transform.name);
            string side = hit.collider.transform.name;
            switch (side)
            {
                case "1 side":
                    rollResult = 6;
                    Debug.Log("rolled a 6");
                    break;
                case "2 side":
                    rollResult = 5;
                    Debug.Log("rolled a 5");
                    break;
                case "3 side":
                    rollResult = 4;
                    Debug.Log("rolled a 4");
                    break;
                case "4 side":
                    rollResult = 3;
                    Debug.Log("rolled a 3");
                    break;
                case "5 side":
                    rollResult = 2;
                    Debug.Log("rolled a 2");
                    break;
                case "6 side":
                    rollResult = 1;
                    Debug.Log("rolled a 1");
                    break;
            }
            clock = 0;
            onDiceFinishRoll?.Invoke(rollResult);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioClip impactSound;
        if (collision.gameObject.tag == "Dice" && transform.position.z < collision.transform.position.z)
            impactSound = diceHit;
        else
            impactSound = diceGround;

        source.PlayOneShot(impactSound);
    }
}
