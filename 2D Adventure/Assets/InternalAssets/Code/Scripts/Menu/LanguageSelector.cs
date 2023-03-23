using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    private String[] languages = { "English", "Italiano" };
    public TextMeshProUGUI selectedLang;
    private int index;

    private void Start()
    {
        index = 0;
        selectedLang.text = languages[index];
    }

    public void SwitchLang()
    {
        index++;
        selectedLang.text = languages[index % languages.Length]; 
    }
}
