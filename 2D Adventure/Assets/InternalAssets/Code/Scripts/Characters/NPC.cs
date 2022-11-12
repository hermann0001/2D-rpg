using UnityEngine;


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

    private void Update()
    {
        if (checkForSkip())
        {
            DialogueSystem.Instance.skipButton.onClick.Invoke();
        }
        if (checkForTalk())
        {
            DialogueSystem.Instance.addNewDialogue(dialogue, npcName, characterSprite, textColor, textFont, sound);
            startedTalking = true;
        }
    }

    private bool checkForTalk()
    {
        return playerIsClose && Input.GetKeyDown(KeyCode.Return) && !startedTalking;
    }
    private bool checkForSkip()
    {
        return playerIsClose && startedTalking && Input.GetKeyDown(KeyCode.Return);
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
            startedTalking = false;

        }
    }
}
