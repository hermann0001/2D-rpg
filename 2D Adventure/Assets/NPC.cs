using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialogueSystem;
    public DialogueLine dialogue;
    private bool playerIsClose;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            Instantiate(DialogueSystem);
            dialogue = DialogueSystem.GetComponentInChildren<DialogueLine>();
            if (!DialogueSystem.activeInHierarchy)
            {
                DialogueSystem.SetActive(true);
                dialogue.Invoke("startDialogue", 0.1f);
                Debug.Log(dialogue.GetComponentInChildren<Text>().tag);
            }
            Debug.Log(dialogue.finished);
            //TODO: SE IL DIALOGO FINISCE DESTROY(DIALOGUESYSTEM)
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
            playerIsClose = false;
    }
}
