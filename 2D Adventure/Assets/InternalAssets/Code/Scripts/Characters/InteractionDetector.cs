using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> interactable_in_range = new List<IInteractable>();

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.interact.WasPerformedThisFrame() && interactable_in_range.Count > 0)
        {
            var interactable = interactable_in_range[0];
            interactable.Interact();
            //if (!interactable.CanInteract())
            //    interactable_in_range.Remove(interactable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
            interactable_in_range.Add(interactable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if(interactable != null && interactable_in_range.Contains(interactable))
            interactable_in_range.Remove(interactable); 
    }
}
