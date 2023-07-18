using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> interactable_in_range = new List<IInteractable>();

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkForDialogueInteractions();

        if (PlayerController.interact.WasPressedThisFrame() && interactable_in_range.Count > 0)
        {
            var interactable = interactable_in_range[0];
            interactable.Interact();

            if (!interactable.CanInteract())
                interactable_in_range.Remove(interactable);
        }
    }

    private void checkForDialogueInteractions()
    {
        var interactable = transform.GetComponentInParent<IInteractable>();

        if (DialogueSystem.Instance.isPanelActive() && !interactable_in_range.Contains(interactable) && interactable.GetType().Equals(typeof(PlayerDialogues)))
            interactable_in_range.Add(interactable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract() && !collision.CompareTag("SceneTransition"))
            interactable_in_range.Add(interactable);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if(interactable != null && interactable_in_range.Contains(interactable))
            interactable_in_range.Remove(interactable); 
    }
}
