using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> interactable_in_range = new List<IInteractable>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Interact") && interactable_in_range.Count > 0)
        {
            Debug.Log("pressed the button!");
            var interactable = interactable_in_range[0];
            interactable.Interact();
            if (!interactable.CanInteract())
            {
                interactable_in_range.Remove(interactable);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.ToString());
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            Debug.Log("added to the list!");
            interactable_in_range.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if(interactable != null && interactable_in_range.Contains(interactable))
        {
            interactable_in_range.Remove(interactable); 
        }
    }
}
