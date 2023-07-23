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
        if(eventScriptableObject.electricity_restored == false)
        {
            lightsOffEmergency();
        }
        else
        {
            lightsOn();
        }
    }

    public void FlickerAndTurnOff()
    {
        StartCoroutine(IEFlicker());
    }

    private IEnumerator IEFlicker()
    {
        yield return new WaitForSeconds(5f);


        lightsOffEmergency();
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
        bathroomLight.GetComponent<BathroomLight>().StartFlicker();
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
    }

    public void lightsOffTotal()
    {
        gameObject.GetComponent<Light2D>().color = noLightColor;
        ambientLight.SetActive(false);
        playerLight.SetActive(true);
    }
}
