using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [HideInInspector]
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();
    public AudioClip sound;
    public Button skipButton;

    public Text textHolder, textName;
    public float delay;

    private int index;

    public Image imageHolder;

    // Start is called before the first frame update
    private void Awake()
    {
        textHolder = dialoguePanel.transform.Find("Text").GetComponent<Text>(); //dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        textName = dialoguePanel.transform.Find("Name").GetComponent<Text>();
        imageHolder = dialoguePanel.transform.Find("NpcImage").GetComponent<Image>(); //dialoguePanel.transform.FindChild("Image").GetComponent<Text>();
        dialoguePanel.SetActive(false);
        skipButton = dialoguePanel.transform.Find("Skip").GetComponent<Button>();
        skipButton.onClick.AddListener(delegate { CompleteOrSkip(); });

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void addNewDialogue(string[] lines, string name, Sprite characterSprite, Color textColor, Font textFont, AudioClip sound)
    {
        index = 0;

        this.npcName = name;
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
        textHolder.color = textColor;
        textHolder.font = textFont;
        textHolder.text = String.Empty;
        textName.font = textFont;
        this.sound = sound;

        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        createDialogue();
    }

    public void createDialogue()
    {
        textName.text = npcName;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        //iterates on each letter of string $dialogueLines[index] and add them to textHolder.text each $delay seconds
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            textHolder.text += c;
            SoundManager.Instance.PlaySound(sound);
            yield return new WaitForSeconds(delay);
        }
    }

    private void CompleteOrSkip()
    {
        if (textHolder.text.Equals(dialogueLines[index]))
            NextLine();
        else
        {
            StopAllCoroutines();
            textHolder.text = dialogueLines[index];
        }
    }

    private void NextLine()
    {
        if (index < dialogueLines.Count - 1)
        {
            index++;
            textHolder.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
            dialoguePanel.SetActive(false);
    }
}
