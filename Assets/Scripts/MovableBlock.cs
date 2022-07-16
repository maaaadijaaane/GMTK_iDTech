using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovableBlock : MonoBehaviour
{
    private Rigidbody rb;
    public UnityEvent onBlockDropped;
    public UnityEvent onBlockRotate;
    BoxCollider blockCollider;
    AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        blockCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
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
                if(Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    Dropped();
                    MouseManager.dragging = false;
                }
            }
        }
    }

    public void Dropped()
    {
        blockCollider.isTrigger = false;
        rb.isKinematic = false;
        MouseManager.dragging = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        gameObject.layer =  LayerMask.NameToLayer("Default");
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
    }

    private void PlayCollisionSound()
    {
        List<AudioClip> hitSounds = GameManager.Instance.audioManager.bank.blockHits;
        audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)], .2f);
    }
}
