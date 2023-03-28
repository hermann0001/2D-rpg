using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("SpawnRoom");
    }

    public void Continue()
    {
        //
    }

    public void Quit()
    {
        Application.Quit();
    }
}
