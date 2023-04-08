using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool is_interacted;
    bool IInteractable.CanInteract()
    {
        return !is_interacted;
    }

    void IInteractable.Interact()
    {
        Debug.Log("interacted!");
        is_interacted = true;
        animator.SetBool("interacted", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            is_interacted = false;
            animator.SetBool("interacted", false);
        }
    }
}
