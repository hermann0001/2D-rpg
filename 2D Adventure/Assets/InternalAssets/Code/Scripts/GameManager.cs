using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider volumeSlider;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SpawnRoom");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame(GameObject pauseMenuScreen)
    {
        Time.timeScale = 0f;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame(GameObject pauseMenuScreen)
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }

    public void GameOver(GameObject gameOverScreen)
    {
        gameOverScreen.SetActive(true);
    }

    public void Retry(GameObject gameOverScreen)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.SetActive(false);
    }
}
