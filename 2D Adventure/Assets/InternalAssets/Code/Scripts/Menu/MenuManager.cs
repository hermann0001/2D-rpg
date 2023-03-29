using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.StartGame();
    }

    public void Continue()
    {
        //GameManager.Instance.LoadLevel();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
