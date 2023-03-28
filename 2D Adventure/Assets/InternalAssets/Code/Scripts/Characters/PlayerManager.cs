using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;
    public void ReturnTitle()
    {
        SceneManager.LoadScene("Menu");
        gameOverScreen.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.SetActive(false);
    }

}
