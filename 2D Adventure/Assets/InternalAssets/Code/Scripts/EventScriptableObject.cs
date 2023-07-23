using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
[CreateAssetMenu(fileName = "EventScriptableObject")]
public class EventScriptableObject : ScriptableObject
{
    [SerializeField] public bool unlock_control;

    private void Awake()
    {
        unlock_control = false;
    }

    public void eat()
    {
        unlock_control = true;
    }

}
