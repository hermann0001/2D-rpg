using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;
    public void Pause()
    {
        Debug.Log(pauseMenuScreen);

        GameManager.Instance.PauseGame(pauseMenuScreen);    
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame(pauseMenuScreen);
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver(gameOverScreen);
    }

    public void Retry()
    {
        GameManager.Instance.Retry(gameOverScreen);
    }

    public void ReturnTitle()
    {
        Debug.Log("pressed");
        GameManager.Instance.LoadMenu();
        gameOverScreen.SetActive(false);
    }
}
