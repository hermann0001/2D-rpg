using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider musicSlider, soundSlider;
    public AudioMixer myMixer;
    public TextMeshProUGUI musicSliderText, soundSliderText;
    public GameObject optionsPanel;
    public SettingsScriptableObject settingsScriptableObject;

    private void Start()
    {
        AudioManager.instance.Play("MenuMusic");
        musicSliderText.text = ((int)Mathf.Lerp(0, 100f, settingsScriptableObject.music_value)).ToString() + "%";
        soundSliderText.text = ((int)Mathf.Lerp(0, 100f, settingsScriptableObject.sound_value)).ToString() + "%";
        musicSlider.SetValueWithoutNotify(settingsScriptableObject.music_value);
        soundSlider.SetValueWithoutNotify(settingsScriptableObject.sound_value);
        myMixer.SetFloat("MusicVolume", Mathf.Log10(settingsScriptableObject.music_value) * 20);
        myMixer.SetFloat("SoundVolume", Mathf.Log10(settingsScriptableObject.sound_value) * 20);

    }
    public void NewGame()
    {
        GameManager.Instance.StartGame();
    }

    public void Continue()
    {
        //AudioManager.instance.Play("Click");
        //GameManager.Instance.LoadLevel();
        //AudioManager.instance.Play("MenuMusic");

    }

    public void Quit()
    {
        AudioManager.instance.Play("Click");
        AudioManager.instance.Stop("MenuMusic");
        GameManager.Instance.Quit();
    }

    public void Settings()
    {
        AudioManager.instance.Play("Click");
        optionsPanel.SetActive(true);
    }

    public void SetMusicVolume()
    {
        myMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        int valueToShow = (int) Mathf.Lerp(0, 100f, musicSlider.value);
        settingsScriptableObject.music_value = musicSlider.value;
        musicSliderText.text = valueToShow.ToString() + "%";
    }

    public void SetSoundVolume()
    {
        myMixer.SetFloat("SoundVolume", Mathf.Log10(soundSlider.value) * 20);
        settingsScriptableObject.sound_value = soundSlider.value;
        int valueToShow = (int)Mathf.Lerp(0, 100f, soundSlider.value);
        soundSliderText.text = valueToShow.ToString() + "%";
    }
}
