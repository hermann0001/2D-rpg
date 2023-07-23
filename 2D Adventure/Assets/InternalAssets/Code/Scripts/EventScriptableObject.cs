using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
[CreateAssetMenu(fileName = "EventScriptableObject", menuName = "ScriptableObjects/EventScriptableObject")]
public class EventScriptableObject : ScriptableObject
{
    [SerializeField] public bool unlock_control;
    [SerializeField] public bool food_trigger;
    [SerializeField] public bool electricity_restored;

    private void Awake()
    {
        unlock_control = false;
        food_trigger = false;
        electricity_restored = true;
    }

    public void eat()
    {
        food_trigger = true;
    }

}
