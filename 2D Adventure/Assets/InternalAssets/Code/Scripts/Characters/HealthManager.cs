using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private PlayerManager playerManager;
    public float currentHealth { get; private set; }

    /*
    public Image[] hp = new Image[3];
    public Sprite[] emptyBar = new Sprite[3];
    public Sprite[] fullBar = new Sprite[3];

    public Image healthStatus;
    public Sprite fullHealthStatus;
    public Sprite missingHealthStatus;
    public Sprite lastHealthStatus;*/

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage()
    {
        currentHealth = Mathf.Clamp(currentHealth - 1, 0, startingHealth);
        if(currentHealth > 0)
        {
            //play sound hurt
        }
        else
        {
            playerManager.GameOver();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage();
        
        /*
        Debug.Log("hp length: " + hp.Length);
        Debug.Log("fullbar length: " + fullBar.Length);
        Debug.Log("emptybar length: " + emptyBar.Length);

        switch (currentHealth)
        {
            case 3:
                hp[0].sprite = fullBar[0];
                hp[1].sprite = fullBar[1];
                hp[2].sprite = fullBar[2];

                healthStatus.sprite = fullHealthStatus;
                break;
            case 2:
                hp[0].sprite = fullBar[0];
                hp[1].sprite = fullBar[1];
                hp[2].sprite = emptyBar[2];

                healthStatus.sprite = missingHealthStatus;
                break;
            case 1:
                hp[0].sprite = fullBar[0];
                hp[1].sprite = emptyBar[1];
                hp[2].sprite = emptyBar[2];

                healthStatus.sprite = lastHealthStatus;
                break;
            case 0:
                hp[0].sprite = emptyBar[0];
                hp[1].sprite = emptyBar[1];
                hp[2].sprite = emptyBar[2];
                break;
        }
        */

    }
}