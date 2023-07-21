using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomLight : MonoBehaviour
{
    public void StartFlicker()
    {
        StartCoroutine(IEFlicker());
    }

    public void StopFlicker()
    {
        StopCoroutine(IEFlicker());
    }

    private IEnumerator IEFlicker()
    {
        yield return new WaitForSecondsRealtime(3.7f);
        while (true)
        {
            gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 0.5f));
            gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 0.5f));

        }
    }
}
