using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool is_interacted;
    private BoxCollider2D boxCollider;
    public bool CanInteract()
    {
        return !is_interacted;
    }

    public void Interact()
    {
        is_interacted = true;
        animator.SetBool("interacted", true);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            is_interacted = false;
            animator.SetBool("interacted", false);
            boxCollider.enabled = true;
        }
    }
}
