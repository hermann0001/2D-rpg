using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
[CreateAssetMenu(fileName = "SettingsScriptableObject", menuName = "ScriptableObjects/SettingsScriptableObject")]
public class SettingsScriptableObject : ScriptableObject
{
    [SerializeField] public string language;
    [SerializeField] public float music_value;
    [SerializeField] public float sound_value;
}
