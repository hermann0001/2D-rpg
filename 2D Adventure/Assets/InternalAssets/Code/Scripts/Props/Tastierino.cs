using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tastierino : MonoBehaviour, IInteractable
{
    [SerializeField] private EventScriptableObject eventScriptableObject;
    [SerializeField] private GameObject controlRoomTrigger;
    [SerializeField] private GlobalLight globalLight;

    private void Start()
    {
        if (eventScriptableObject.food_trigger == false)
        {
            eventScriptableObject.unlock_control = false;
            eventScriptableObject.electricity_restored = true;
        }

    }

    public bool CanInteract()
    {
        return eventScriptableObject.electricity_restored == false;
    }

    public void Interact()
    {
        Debug.Log("Interacted!");
        StartCoroutine(IEUnlockControl());
    }
    
    public IEnumerator IEUnlockControl()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("DigitSound");
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("DigitSound");
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("DigitSound");
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("GeneratorSound");
        eventScriptableObject.unlock_control = true;
        eventScriptableObject.electricity_restored = true;
        controlRoomTrigger.SetActive(true);
        globalLight.lightsOffEmergency();
    }
}
