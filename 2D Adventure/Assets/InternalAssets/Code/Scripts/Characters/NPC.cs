using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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

    private void Update()
    {
        if (checkForSkip())
        {
            DialogueSystem.Instance.skipButton.onClick.Invoke();
            Debug.Log("porcodio");
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
            playerIsClose = false;
    }
}
