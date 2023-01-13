using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    private bool playerIsClose;
    private bool startedTalking;
    private bool press = false;

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

    private void Update()
    {
        if (checkForSkip())
        {
            Invoke("callToSkip", 0.6f);
        }
        if (checkForTalk())
        {
            startedTalking = true;
            DialogueSystem.Instance.addNewDialogue(dialogue, npcName, characterSprite, textColor, textFont, sound);
        }
    }

    private bool checkForTalk()
    {
        return playerIsClose && press && !startedTalking;
    }
    private bool checkForSkip()
    {
        return playerIsClose && startedTalking && press;
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

    public void interactableButtonPressed() {press = true;}

    public void interactableButtonRelease() {press = false;}

    void callToSkip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }
}
