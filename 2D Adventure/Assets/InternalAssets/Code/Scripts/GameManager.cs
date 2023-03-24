using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int startingSceneIndex = 1;
    private bool _isGameOver;
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(startingSceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public bool isGameOver()
    {
        return _isGameOver;
    }


}
