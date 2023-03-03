using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    private bool playerIsClose;
    private bool startedTalking;

    [Header("Dialogue")]
    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private Color textColor;
    [SerializeField]
    private Font textFont;

    [Header("Name")]
    [SerializeField]
    private string npcName;

    [Header("Sound")]
    [SerializeField]
    private AudioClip sound;

    [Header("Image")]
    [SerializeField]
    private Sprite characterSprite;

    public void Talk()
    {
        if (!startedTalking)
        {
            startedTalking = true;
            DialogueSystem.Instance.addNewDialogue(dialogue, npcName, characterSprite, textColor, textFont, sound);
        }
        else
            Skip();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            DialogueSystem.Instance.dialoguePanel.SetActive(false);
            //SoundManager.Instance.StopSound();
            DialogueSystem.Instance.StopAllCoroutines();
            startedTalking = false;
        }
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }
}
