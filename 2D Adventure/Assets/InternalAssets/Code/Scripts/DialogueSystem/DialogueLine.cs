using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : MonoBehaviour
    {
        public Text textHolder;
        public string[] lines;

        private int index;
        public bool finished;
        [Header("Text Options")]
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Parameters")]
        [SerializeField] private float delay;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Sound")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
            textHolder.text = string.Empty;
        }

        private void Update()
        {
            //if you E it will skip to the next line or if the line is not over yet, it will be shown entirely
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textHolder.text.Equals(lines[index]))
                    NextLine();
                else
                {
                    StopAllCoroutines();
                    textHolder.text = lines[index];
                }
            }
        }

        private IEnumerator TypeLine()
        {
            //iterates on each letter of string $lines[index] and add them to textHolder.text each $delay seconds
            foreach (char c in lines[index].ToCharArray())
            {
                textHolder.text += c;
                yield return new WaitForSeconds(delay);
            }
        }

        private void startDialogue()
        {
            index = 0;
            finished = false;
            StartCoroutine(TypeLine());
        }

        private void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                textHolder.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                //deactivates the first gameObject's parents (in this case the dialoguePanel is parent of dialogueText
                finished = true;
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}

