using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Vector2 startingPos;

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
        startingPos = new Vector2(0.81f, -0.739f);
        AudioManager.instance.Play("Click");
        AudioManager.instance.Stop("MenuMusic");
        SceneManager.LoadScene("SpawnRoom");
        AudioManager.instance.Play("SpawnRoomMusic");
    }
    public void Quit()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

    public void LoadMenu()
    {
        AudioManager.instance.Stop();
        AudioManager.instance.Play("Click");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
        AudioManager.instance.Play("MenuMusic");
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
        AudioManager.instance.Stop();
        gameOverScreen.SetActive(true);
        AudioManager.instance.Play("GameOverMusic");
    }

    public void Retry(GameObject gameOverScreen)
    {
        AudioManager.instance.Stop();
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        string activeSceneMusicName = SceneManager.GetActiveScene().name;
        activeSceneMusicName += "Music";
        AudioManager.instance.Play(activeSceneMusicName);
        gameOverScreen.SetActive(false);
    }

    public void Load(string sceneName)
    {
        AudioManager.instance.Play("DoorCloseSound");
        AudioManager.instance.Play(sceneName + "Music");
    }
}
