using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;

    public static bool firstDialogueShown = false;

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

    public void Pause()
    {
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
