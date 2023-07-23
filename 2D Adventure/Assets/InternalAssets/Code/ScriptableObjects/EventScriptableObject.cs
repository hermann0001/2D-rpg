using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
[CreateAssetMenu(fileName = "EventScriptableObject", menuName = "ScriptableObjects/EventScriptableObject")]
public class EventScriptableObject : ScriptableObject
{
    [SerializeField] public bool unlock_control;
    [SerializeField] public bool food_trigger;
    [SerializeField] public bool electricity_restored;
    [SerializeField] public bool talked_to_pc;
    [SerializeField] public bool first_scary_audio_played;
    [SerializeField] public bool talked_to_wetcher;

    private void Awake()
    {
        unlock_control = false;
        food_trigger = false;
        electricity_restored = true;
        talked_to_pc = false;
        first_scary_audio_played = false;
        talked_to_wetcher = false;
    }

    public void eat()
    {
        food_trigger = true;
        AudioManager.instance.Play("CrunchingSound");
    }
}
