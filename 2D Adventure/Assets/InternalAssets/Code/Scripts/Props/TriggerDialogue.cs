using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] string tagFilter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !collision.gameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !collision.gameObject.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
    }
}
