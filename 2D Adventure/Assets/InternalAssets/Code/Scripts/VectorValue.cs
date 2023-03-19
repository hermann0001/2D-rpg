using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 playerInitialValue;
    public Vector2 playerDefaultValue;

    public void OnAfterDeserialize()
    {
        playerInitialValue = playerDefaultValue;
    }

    public void OnBeforeSerialize()
    {
        return;
    }
    //public bool needText;
    //public string placeName;
}
