using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadDelay());
    }

    public IEnumerator LoadDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        GameManager.Instance.Load(SceneManager.GetActiveScene().name);
    }
}
