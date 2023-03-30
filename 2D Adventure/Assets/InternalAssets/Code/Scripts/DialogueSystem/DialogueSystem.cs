using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [HideInInspector]
    public static DialogueSystem Instance { get; set; }

    [SerializeField]
    public GameObject dialoguePanel;                               //the entire panel
    public Button skipButton;                                       //an invisible button to skip
    private Text textHolder, textName;                              //textHolder is the text box where main text will be shown; textName is the text box where the name of who is talking will be shown
    private string npcName;                                         //this is the name of who is talking         
    private List<string> dialogueLines = new List<string>();        //this is the text that will be written in the textHolder
    private AudioClip sound;                                        //a sound for each character displayed
    private float delay;                                            //delay between each charater displayed
    private Image imageHolder;                                      //an holder for the npc image 

    private int index;

    // Start is called before the first frame update
    private void Awake()
    {
        dialoguePanel.SetActive(false);
        textHolder = dialoguePanel.transform.Find("Text").GetComponent<Text>(); //dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        textName = dialoguePanel.transform.Find("Name").GetComponent<Text>();
        imageHolder = dialoguePanel.transform.Find("NpcImage").GetComponent<Image>(); //dialoguePanel.transform.FindChild("Image").GetComponent<Text
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

        //this.npcName = name
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;

        textHolder.color = textColor;
        textHolder.font = textFont;
        textHolder.text = String.Empty;

        textName.text = name;
        textName.font = textFont;

        this.sound = sound;

        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        if (npcName == null)
            dialoguePanel.transform.Find("Name").gameObject.SetActive(false);
        if (imageHolder.sprite == null)
            dialoguePanel.transform.Find("NpcImage").gameObject.SetActive(false);
            

        createDialogue();
    }

    public void createDialogue()
    {
        //textName.text = npcName;
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
