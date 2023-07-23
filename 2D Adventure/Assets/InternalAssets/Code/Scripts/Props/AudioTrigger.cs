using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private EventScriptableObject eventScriptableObject;
    [SerializeField] private GameObject wetcher;
    [SerializeField] private PlayerDialogues playerDialogues;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionDetector") && eventScriptableObject.first_scary_audio_played == false && eventScriptableObject.unlock_control == true)
        {
            AudioManager.instance.Play("GlassBreakSound");
            eventScriptableObject.first_scary_audio_played = true;
            StartCoroutine(waiterForDialogue());
            wetcher.SetActive(true);
        }
    }

    private IEnumerator waiterForDialogue()
    {
        yield return new WaitForSeconds(3.5f);
        playerDialogues.ControllaBagno();
    }
}
