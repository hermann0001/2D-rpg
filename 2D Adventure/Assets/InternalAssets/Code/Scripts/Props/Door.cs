using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            animator.SetBool("interacted", true);
            AudioManager.instance.Play("DoorOpenSound");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            animator.SetBool("interacted", false);
            AudioManager.instance.Play("DoorCloseSound");
        }
    }
}
