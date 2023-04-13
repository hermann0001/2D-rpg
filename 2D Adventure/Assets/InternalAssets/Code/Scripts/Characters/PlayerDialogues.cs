using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogues : MonoBehaviour, IInteractable
{
    private bool first_dialogue_shown = false;
    private bool is_talking = false;

    [Header("Dialogue Settings")]
    [SerializeField] private Sprite dialogueSpriteIcon;
    [SerializeField] private Font dialogueFont;
    [SerializeField] private Color dialogueTextColor;
    [SerializeField] private AudioClip typingSound;

    // Update is called once per frame
    void Update()
    {
        if (!first_dialogue_shown)
        {
            first_dialogue_shown = true;
            StartCoroutine(CreateFirstDialogue());
        }
    }

    public IEnumerator CreateFirstDialogue()
    {
        is_talking = true;
        yield return new WaitForSecondsRealtime(3f);
        string[] lines = { "dovrei controllare l'ordine del giorno..." };
        DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
    }

    public void Interact()
    {
        Debug.Log("interacted!");
        if (is_talking)
            Skip();
    }

    public bool CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive();
    }
    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }

    public bool isFirstDialogueShown()
    {
        return first_dialogue_shown;
    }

}
