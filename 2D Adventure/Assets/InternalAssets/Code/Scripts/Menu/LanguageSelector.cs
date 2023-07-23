using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private string[] languages = { "English", "Italiano" };
    [SerializeField] private TextMeshProUGUI selectedLang;
    [SerializeField] private SettingsScriptableObject settingsScriptableObject;
    
    private int index;

    private void Start()
    {
        index = 1;
        selectedLang.text = languages[index];
        settingsScriptableObject.language = languages[index];
    }

    public void SwitchLang()
    {
        index++;
        selectedLang.text = languages[index % languages.Length];
        settingsScriptableObject.language = languages[index % languages.Length];
    }
}
