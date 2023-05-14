using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogues : MonoBehaviour, IInteractable
{
    public static PlayerDialogues Instance { get; private set; }     
    private bool first_dialogue_shown = false;
    private bool is_talking = false;

    [Header("Dialogue Settings")]
    [SerializeField] private Sprite dialogueSpriteIcon;
    [SerializeField] private Font dialogueFont;
    [SerializeField] private Color dialogueTextColor;
    [SerializeField] private AudioClip typingSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        CreaExitBlock();
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
    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }

    public bool isFirstDialogueShown()
    {
        return first_dialogue_shown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExitBlock"))
        {
            string[] lines = { "Forse dovrei controllare l'ordine del giorno prima... Dovrebbe stare nelle mail." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
            transform.position = new Vector3(1.928f, -1.968f, transform.position.z);
        }

        if (collision.CompareTag("Dialogo1"))
        {
            string[] lines = { "Che fame..." };
            DialogueSystem.Instance.addNewDialogue(lines, dialogueSpriteIcon, dialogueTextColor, dialogueFont, typingSound);
        }
    }

}
