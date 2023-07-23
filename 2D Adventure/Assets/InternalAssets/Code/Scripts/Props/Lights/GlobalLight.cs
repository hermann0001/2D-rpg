using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    [SerializeField] GameObject playerLight;
    [SerializeField] GameObject bathroomLight;
    [SerializeField] GameObject ambientLight;
    [SerializeField] Color noLightColor;
    [SerializeField] Color lightsOnColor;
    [SerializeField] PlayerDialogues p_dialogues;
    [SerializeField] EventScriptableObject eventScriptableObject;

    private void Start()
    {
        if(eventScriptableObject.electricity_restored == true && eventScriptableObject.food_trigger == false)
        {
            lightsOn();
        }
        else if(eventScriptableObject.electricity_restored == false && eventScriptableObject.food_trigger == true)
        {
            lightsOffTotal();
        }
        else if(eventScriptableObject == true && eventScriptableObject.food_trigger == true)
        {
            lightsOffEmergency();
        }
    }

    public void FlickerAndTurnOff()
    {
        StartCoroutine(IEFlicker());
    }

    private IEnumerator IEFlicker()
    {
        yield return new WaitForSeconds(5f);

        lightsOffTotal();
        yield return new WaitForSeconds(0.6f);
        lightsOn();
        yield return new WaitForSeconds(0.4f);
        lightsOffTotal();
        yield return new WaitForSeconds(0.5f);
        lightsOffTotal();
        yield return new WaitForSeconds(0.6f);
        lightsOn();
        yield return new WaitForSeconds(0.4f);
        lightsOffTotal();
        yield return new WaitForSeconds(0.7f);
        lightsOn();

        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 1.8f));
        lightsOffTotal();
        yield return new WaitForSeconds(2f);
        p_dialogues.GeneratoreDiRiserva();
        eventScriptableObject.electricity_restored = false;
    }

    public void lightsOn()
    {
        gameObject.GetComponent<Light2D>().color = lightsOnColor;
        playerLight.SetActive(false);
        ambientLight.SetActive(false);
    }

    public void lightsOffEmergency()
    {
        gameObject.GetComponent<Light2D>().color = noLightColor;
        playerLight.SetActive(true);
        ambientLight.SetActive(true);
        if(bathroomLight != null) StartCoroutine(IEbathroomFlicker());
    }

    public void lightsOffTotal()
    {
        gameObject.GetComponent<Light2D>().color = noLightColor;
        ambientLight.SetActive(false);
        playerLight.SetActive(true);
    }

    public IEnumerator IEbathroomFlicker()
    {
        while (true)
        {
            bathroomLight.SetActive(false);
            yield return new WaitForSeconds(1f);
            bathroomLight.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
