using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : Interactable
{

    private Rigidbody rb;
    public float rollForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void use()
    {
        roll();
    }

    public void roll()
    {
        Debug.Log("Rolled");
        rb.AddForce(0, rollForce, 0, ForceMode.Impulse);
        rb.AddTorque(Random.Range(-rollForce, rollForce), 0, Random.Range(-rollForce, rollForce));
    }

}
