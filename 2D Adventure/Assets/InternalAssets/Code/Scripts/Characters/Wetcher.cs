using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wetcher : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject wetcherTrigger;
    [SerializeField] private Animator animator;
    [SerializeField] private string[] lines;
    [SerializeField] private Color textColor;
    [SerializeField] private Font textFont;
    [SerializeField] private string sound;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private GameObject player;
    [SerializeField] private HealthManager healthManager;

    [SerializeField] private EventScriptableObject eventScriptableObject;

    private bool is_interacted;

    // Start is called before the first frame update
    void Start()
    {
        wetcherTrigger.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LookLeft()
    {
        animator.SetFloat("Horizontal", -1f);
        animator.SetFloat("Vertical", 0f);
    }

    public void LookUp()
    {
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", 1f);
    }

    public void Talk()
    {
        AudioManager.instance.Stop();
        AudioManager.instance.Play("WetcherMusic");
        DialogueSystem.Instance.addNewDialogue(lines, characterSprite, textColor, textFont, sound);
        is_interacted = true;
        eventScriptableObject.talked_to_wetcher = true;
    }

    public void Interact()
    {
        if (is_interacted && DialogueSystem.Instance.isPanelActive())
            Skip();
        else if (is_interacted && !DialogueSystem.Instance.isPanelActive())
            eventScriptableObject.talked_to_wetcher = true;
        else if (eventScriptableObject.talked_to_wetcher == true)
            player.transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
    }

    public bool CanInteract()
    {
        return DialogueSystem.Instance.isPanelActive() || !is_interacted;
    }

    private void Skip() { DialogueSystem.Instance.skipButton.onClick.Invoke(); }

    public void Disappear()
    {
        StartCoroutine(IEDisappear());
    }

    private IEnumerator IEDisappear()
    {
        AudioManager.instance.Stop("WetcherMusic");
        AudioManager.instance.Play("DormsMusic");
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
        wetcherTrigger.SetActive(false);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerDialogues>().PensieroDopoWetcherBagno();
    }

    public void reachPlayerAndKill()
    {
        LookLeft();
        transform.position = new Vector3(player.transform.position.x + 0.25f, player.transform.position.y, player.transform.position.z);
        AudioManager.instance.Play("WetcherJumpscareSound");
        StartCoroutine(waiter());
    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerManager.Instance.GameOver();
    }
}
