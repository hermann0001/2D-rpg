using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public static bool first_spawnroom_dialogue_shown = false;
    public static bool first_dorm_dialogue_shown = false;
    public static bool bathroom_visited = false;


    [SerializeField] private GameObject player;

    [Header("Panels")]
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject optionsPanel;

    [Header("AudioSliders")]
    public Slider musicSlider;
    public Slider soundSlider;
    public AudioMixer myMixer;
    public TextMeshProUGUI musicSliderText, soundSliderText;

    public static bool firstDialogueShown = false;

    private void Awake()
    {
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        musicSliderText.text = ((int)Mathf.Lerp(0, 100f, musicSlider.value)).ToString() + "%";
        soundSliderText.text = ((int)Mathf.Lerp(0, 100f, soundSlider.value)).ToString() + "%";
    }



    public void Pause()
    {
        GameManager.Instance.PauseGame(pauseMenuPanel);    
    }

    public void OpenSettings()
    {
        AudioManager.instance.Play("Click");
        optionsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        AudioManager.instance.Play("Click");
        optionsPanel.SetActive(false);
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame(pauseMenuPanel);
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver(gameOverPanel);
        Destroy(player);
    }

    public void Retry()
    {
        GameManager.Instance.Retry(gameOverPanel);
    }

    public void ReturnTitle()
    {
        GameManager.Instance.LoadMenu();
        gameOverPanel.SetActive(false);
        Destroy(gameObject);
    }

    public void SetMusicVolume()
    {
        myMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        int valueToShow = (int)Mathf.Lerp(0, 100f, musicSlider.value);
        musicSliderText.text = valueToShow.ToString() + "%";
    }

    public void SetSoundVolume()
    {
        myMixer.SetFloat("SoundVolume", Mathf.Log10(soundSlider.value) * 20);
        int valueToShow = (int)Mathf.Lerp(0, 100f, soundSlider.value);
        soundSliderText.text = valueToShow.ToString() + "%";
    }

    public Vector2 GetStartingPosition()
    {
        return GameManager.startingPos;
    }
}
