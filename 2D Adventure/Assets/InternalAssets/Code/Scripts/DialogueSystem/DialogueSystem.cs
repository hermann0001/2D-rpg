using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [HideInInspector]
    public static DialogueSystem Instance { get; private set; }                     //singleton class

    public GameObject dialoguePanel;                                                //the entire panel
    public Button skipButton;                                                       //an invisible button to skip

    [SerializeField] private Text textHolder, textName;                              //textHolder is the text box where main text will be shown; textName is the text box where the name of who is talking will be shown
    [SerializeField] private List<string> dialogueLines = new List<string>();        //this is the text that will be written in the textHolder
    [SerializeField] private AudioClip sound;                                        //a sound for each character displayed
    [SerializeField] private float delay;                                            //delay between each charater displayed
    [SerializeField] private Image imageHolder;                                      //an holder for the npc image 

    private GameObject controller;

    private int index;
    
    private void Awake()
    {
        index = 0;
        dialoguePanel = GameObject.FindGameObjectWithTag("DialoguePanel");
        dialoguePanel.SetActive(false);
        textHolder = dialoguePanel.transform.Find("Text").GetComponent<Text>(); //dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        imageHolder = dialoguePanel.transform.Find("NpcImage").GetComponent<Image>(); //dialoguePanel.transform.FindChild("Image").GetComponent<Text
        skipButton = dialoguePanel.transform.Find("Skip").GetComponent<Button>();
        skipButton.onClick.AddListener(delegate { CompleteOrSkip(); });
        controller = GameObject.FindGameObjectWithTag("TouchController");

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    //overload dialogue with npc
    public void addNewDialogue(string[] lines, Sprite characterSprite, Color textColor, Font textFont, AudioClip sound)
    {
        index = 0;


        //this.npcName = name
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;

        textHolder.color = textColor;
        textHolder.font = textFont;
        textHolder.text = String.Empty;

        this.sound = sound;

        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        createDialogue();
    }

    //overload for dialogue with props
    public void addNewDialogue(string[] lines, Color textColor, Font textFont)
    {
        index = 0;

        textHolder.color = textColor;
        textHolder.font = textFont;
        textHolder.text = String.Empty;

        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        dialoguePanel.transform.Find("NpcImage").gameObject.SetActive(false);

        createDialogue();
    }

    public void createDialogue()
    {
        //textName.text = npcName;
        dialoguePanel.SetActive(true);
        controller.SetActive(false);
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        //iterates on each letter of string $dialogueLines[index] and add them to textHolder.text each $delay seconds
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            textHolder.text += c;
            if(sound != null)
                SoundManager.Instance.PlaySound(sound);
            yield return new WaitForSeconds(delay);
        }
    }

    private void CompleteOrSkip()
    {
        if(dialogueLines.Count > 0)
        {
            if (textHolder.text.Equals(dialogueLines[index]))
                NextLine();
            else
            {
                StopAllCoroutines();
                textHolder.text = dialogueLines[index];
            }
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
        {
            dialoguePanel.SetActive(false);
            controller.SetActive(true);
            sound = null;
        }
    }

    public bool isPanelActive()
    {
        return dialoguePanel.activeSelf;
    }
}
