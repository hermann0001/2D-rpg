using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private EventScriptableObject eventScriptableObject;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector") && eventScriptableObject.first_scary_audio_played == false && eventScriptableObject.food_trigger == true)
        {
            AudioManager.instance.Play("GlassBreakSound");
            eventScriptableObject.first_scary_audio_played = true;
        }
    }
}
