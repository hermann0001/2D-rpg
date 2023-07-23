using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
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

    [Header("Sound")]
    [SerializeField]
    private string sound;

    [Header("Image")]
    [SerializeField]
    private Sprite characterSprite;

    public void Interact()
    {
        if (playerIsClose)
        {
            if (!startedTalking)
            {
                startedTalking = true;
                DialogueSystem.Instance.addNewDialogue(dialogue, characterSprite, textColor, textFont, sound);
            }
            else
                Skip();
        }
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

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }
}
