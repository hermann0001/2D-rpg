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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
    }

    public void Retry()
    {
        GameManager.Instance.Retry(gameOverPanel);
    }

    public void ReturnTitle()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.LoadMenu();
        Destroy(gameObject);
        Destroy(player);
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
}
