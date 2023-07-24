using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerDialogues dialogues;

    private bool is_interacted;
    public bool CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive() || (!is_interacted);
    }

    public void Interact()
    {
        if (!is_interacted)
        {
            is_interacted = true;
            dialogues.PortaBloccata();
        }
        else
            Skip();
    }

    void Start()
    {
        dialogues = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogues>(); 
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }

}
