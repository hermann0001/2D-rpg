using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;

    [SerializeField] private Sprite dialogueSpriteIcon;
    [SerializeField] private Font dialogueFont;
    [SerializeField] private Color dialogueTextColor;

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

    public IEnumerator CreateFirstDialogue()
    {
        yield return new WaitForSecondsRealtime(3f);
        string[] lines = { "dovrei controllare l'ordine del giorno..." };
        DialogueSystem.Instance.addNewDialogue(lines, "Anastasia", dialogueSpriteIcon, dialogueTextColor, dialogueFont, null);
    }
}
