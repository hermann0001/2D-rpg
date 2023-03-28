using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthManager playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image healthStatus;
    [SerializeField] private Sprite[] heartStatus;


    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 3;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 3;
        int index = Mathf.Clamp((int)playerHealth.currentHealth - 1, 0, 2);
        healthStatus.sprite = heartStatus[index];
    }
}
