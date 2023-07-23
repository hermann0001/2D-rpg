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

    public void FlickerAndTurnOff()
    {
        StartCoroutine(IEFlicker());
    }

    private IEnumerator IEFlicker()
    {
        yield return new WaitForSeconds(5f);


        lightsOff();
        yield return new WaitForSeconds(0.6f);
        lightsOn();
        yield return new WaitForSeconds(0.4f);
        lightsOff();
        yield return new WaitForSeconds(0.5f);
        lightsOff();
        yield return new WaitForSeconds(0.6f);
        lightsOn();
        yield return new WaitForSeconds(0.4f);
        lightsOff();
        yield return new WaitForSeconds(0.7f);
        lightsOn();

        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 1.8f));
        lightsOff();
        bathroomLight.GetComponent<BathroomLight>().StartFlicker();
        p_dialogues.GeneratoreDiRiserva();
    }

    private void lightsOn()
    {
        gameObject.GetComponent<Light2D>().color = lightsOnColor;
        playerLight.SetActive(false);
        ambientLight.SetActive(false);
    }

    private void lightsOff()
    {
        gameObject.GetComponent<Light2D>().color = noLightColor;
        playerLight.SetActive(true);
        ambientLight.SetActive(true);
    }
}
