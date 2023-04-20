using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool is_interacted;

    [Header("Dialogue")]
    [SerializeField] private string[] dialogue;
    [SerializeField] private Color dialogueColor;
    [SerializeField] private Font dialogueFont;

    bool IInteractable.CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive() || (!is_interacted);
    }

    void IInteractable.Interact()
    {
        if (!is_interacted)
        {
            is_interacted = true;
            animator.SetBool("interacted", true);
            DialogueSystem.Instance.addNewDialogue(dialogue, dialogueColor, dialogueFont);
            //GameObject.FindGameObjectWithTag("ExitBlock").GetComponent<BoxCollider2D>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("ExitBlock"));

        }
        else
            Skip();
        
    }
    void Start()
    {
        animator = GetComponent<Animator>();  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector"))
        {
            is_interacted = false;
            animator.SetBool("interacted", false);
            DialogueSystem.Instance.dialoguePanel.SetActive(false);
            //SoundManager.Instance.StopSound();
            DialogueSystem.Instance.StopAllCoroutines();
        }
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }
}
