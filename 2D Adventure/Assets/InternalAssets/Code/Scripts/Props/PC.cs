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
    [SerializeField] private GameObject screenLight;
    [SerializeField] private PlayerDialogues playerDialogues;

    bool IInteractable.CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive() || (!is_interacted);
    }

    void IInteractable.Interact()
    {
        AudioManager.instance.Play("MouseclickSound");
        if (!is_interacted)
        {
            is_interacted = true;
            animator.SetBool("interacted", true);
            DialogueSystem.Instance.addNewDialogue(dialogue, dialogueColor, dialogueFont);
            Destroy(GameObject.FindGameObjectWithTag("ExitBlock"));
            screenLight.SetActive(true);
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
            DialogueSystem.Instance.StopAllCoroutines();
            screenLight.SetActive(false);
            playerDialogues.PensieroRispostaAlMessaggioPC();
        }
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }
}
