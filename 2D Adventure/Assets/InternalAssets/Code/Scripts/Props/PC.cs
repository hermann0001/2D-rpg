using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool is_interacted;

    [Header("Dialogue")]
    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private Color dialogueColor;
    [SerializeField]
    private Font dialogueFont;

    bool IInteractable.CanInteract()
    {
        return !is_interacted;
    }

    void IInteractable.Interact()
    {
        if (!is_interacted)
        {
            is_interacted = true;
            animator.SetBool("interacted", true);
            DialogueSystem.Instance.addNewDialogue(dialogue, dialogueColor, dialogueFont);
        }
        else
            Skip();
        
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
            DialogueSystem.Instance.dialoguePanel.SetActive(false);
            //SoundManager.Instance.StopSound();
            DialogueSystem.Instance.StopAllCoroutines();
        }
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }
}
