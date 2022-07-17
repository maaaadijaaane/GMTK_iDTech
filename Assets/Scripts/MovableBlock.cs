using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovableBlock : MonoBehaviour
{
    public Ability ability = Ability.None;
    public Rigidbody rb;
    public UnityEvent onBlockDropped;
    public UnityEvent onBlockRotate;
    public Collider blockCollider;
    AudioSource audioSource;
    private bool droppable = true;
    private int LayerGround;
    // Ability events
    //public UnityEvent<Ability> addAbility;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        blockCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        LayerGround = LayerMask.NameToLayer("Ground");
        gameObject.tag = "Ground";
        if(ability != Ability.None)
        {
            //addAbility?.Invoke(ability);
            AbilitiesManager.AddAbility(ability);
        }
    }

    public void Moving()
    {
        blockCollider.isTrigger = true;
        if(MouseManager.dragging == true)
        {
            // Create event to do this on object instead
            if(MouseManager.dragging == true)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                mousePos.z = 10f;
                Vector3 mouse = BlockGrid.currentGrid.SnapToGrid(Camera.main.ScreenToWorldPoint(mousePos));
                transform.position = new Vector3(mouse.x, mouse.y, 0.0f);
                if(Mouse.current.leftButton.wasReleasedThisFrame && droppable)
                {
                    Dropped();
                    MouseManager.dragging = false;
                }
            }
        }
    }

    public void Dropped()
    {
        if(UpdatePlayer.activeAbility != Ability.Static)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        gameObject.layer = LayerGround;
        blockCollider.isTrigger = false;
        rb.isKinematic = false;
        MouseManager.dragging = false;
        gameObject.layer = LayerGround;
        onBlockDropped?.Invoke();
        MouseManager.block = null;
    }

    public void Rotate()
    {
        Vector3 rotate = new Vector3(0, 0, 90);
        transform.Rotate(rotate);
        onBlockRotate?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            PlayCollisionSound();
        }
        if(collision.gameObject.layer == LayerGround)
        {
            rb.mass = 5;
        }
    }

    private void PlayCollisionSound()
    {
        List<AudioClip> hitSounds = GameManager.Instance.audioManager.bank.blockHits;
        audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)], .2f);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerGround)
        {
            droppable = false;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerGround)
        {
            droppable = true;
        }
    }
}