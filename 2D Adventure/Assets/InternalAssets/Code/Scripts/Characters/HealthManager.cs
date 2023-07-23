using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }


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
            PlayerManager.Instance.GameOver();
        }

    }

    public void Gain1Hp()
    {
        if (currentHealth < 3 && currentHealth > 0)
            currentHealth++;
    }
}