using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDialogues : MonoBehaviour, IInteractable
{
    private bool is_talking = false;

    [Header("Dialogue Settings")]
    [SerializeField] private Sprite dialogueSpriteIcon;
    [SerializeField] private Font dialogueFont;
    [SerializeField] private Color dialogueTextColor;
    [SerializeField] private string typingSound;
    [SerializeField] private EventScriptableObject eventScriptableObject;

    private void Awake()
    {
        if(eventScriptableObject.talked_to_pc == false && SceneManager.GetActiveScene().name.Equals("SpawnRoom"))
            CreaExitBlock();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("SpawnRoom"))
        {
            StartCoroutine(CreateFirstDialogue());
            eventScriptableObject.talked_to_pc = true;
        }
    }
    private void Update()
    {
        is_talking = DialogueSystem.Instance.isPanelActive();
    }

    public IEnumerator CreateFirstDialogue()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        string[] lines = { "Dovrei controllare l'ordine del giorno..." };
        DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
    }

    public void Interact()
    {
        if (is_talking)
            Skip();
    }

    public bool CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive();
    }
    private void Skip() {
        DialogueSystem.Instance.skipButton.onClick.Invoke(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExitBlock"))
        {
            string[] lines = { "Forse dovrei controllare l'ordine del giorno prima... Dovrebbe stare nelle mail." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
            transform.position = new Vector3(1.928f, -1.968f, transform.position.z);
        }
    }

    private static void CreaExitBlock()
    {
        GameObject exit_block = new GameObject("ExitBlock", typeof(BoxCollider2D));
        exit_block.transform.position = new Vector3(1.921f, -2.323864f, -9.937798f);
        exit_block.tag = "ExitBlock";

        BoxCollider2D boxCollider2D = exit_block.GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(1.301175f, 0.2906704f);
        boxCollider2D.offset = new Vector2(-0.002182484f, -0.1233273f);
        boxCollider2D.edgeRadius = 0.025f;
    }

    public void PensieroRispostaAlMessaggioPC()
    {
        if (!is_talking)
        {
            string[] lines = { "Chissà cosa sarà successo a tutti quanti..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
        }
    }
    public void CreaPrimoDialogoDormitorio()
    {
        if (!PlayerManager.first_dorm_dialogue_shown)
        {
            string[] lines = { "Che fame..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
            PlayerManager.first_dorm_dialogue_shown = true;
        }
    }

    public void VaiAlBagnoPrimaDiPassare()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (!PlayerManager.bathroom_visited && !is_talking)
        {
            string[] lines = { "Forse dovrei prima darmi una rinfrescata al bagno..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
        }
    }

    public void TiStaiSpecchiando()
    {
        if (!is_talking)
        {
            string[] lines = { "Sono io, Anastasia." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
            AudioManager.instance.Play("HandWashingSound");
        }
    }
    public void GeneratoreDiRiserva()
    {
        if (!is_talking)
        {
            string[] lines = { "È la terza volta in questo mese", "dovrebbero rivedere il sistema di alimentazione...", "Ricordo un generatore di riserva nei paraggi." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
        }
    }

    public void ControllaBagno()
    {
        if (!is_talking)
        {
            string[] lines = { "Quel rumore sembrava provenire dal bagno..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
        }
    }
}
