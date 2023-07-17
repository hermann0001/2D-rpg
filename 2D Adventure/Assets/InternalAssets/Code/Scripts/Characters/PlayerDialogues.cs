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
    [SerializeField] private AudioClip typingSound;

    private void Awake()
    {
        if(!PlayerManager.first_spawnroom_dialogue_shown && SceneManager.GetActiveScene().name.Equals("SpawnRoom"))
            CreaExitBlock();
    }


    // Update is called once per frame
    void Update()
    {
        is_talking = DialogueSystem.Instance.isPanelActive();
        if (!PlayerManager.first_spawnroom_dialogue_shown)
        {
            PlayerManager.first_spawnroom_dialogue_shown = true;
            StartCoroutine(CreateFirstDialogue());
        }
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
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (!PlayerManager.bathroom_visited && !is_talking)
        {
            string[] lines = { "Forse dovrei prima darmi una rinfrescata al bagno..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
            //TODO: da controllare se viene aggiunto alla lista di interactable!!
        }
    }
}
