//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class DialogueLine : MonoBehaviour
//{
//    public string[] lines;

//    private int index;

//    public bool finished;

//    [Header("Text Options")]
//    [SerializeField]
//    private Color textColor;

//    [SerializeField]
//    private Font textFont;

//    [Header("Time Parameters")]
//    [SerializeField]
//    private float delay;

//    [Header("Sound")]
//    [SerializeField]
//    private AudioClip sound;

//    [Header("NPC")]
//    [SerializeField]
//    private Sprite characterSprite;

//    [SerializeField]
//    private Image imageHolder;

//    private void Awake()
//    {
//        textHolder = GetComponent<Text>();

//        imageHolder.sprite = characterSprite;
//        imageHolder.preserveAspect = true;
//        textHolder.text = string.Empty;
//    }

//    private void Start()
//    {
//        //if you press E it will skip to the next line or if the line is not over yet, it will be shown entirely
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            if (textHolder.text.Equals(lines[index]))
//                NextLine();
//            else
//            {
//                StopAllCoroutines();
//                textHolder.text = lines[index];
//            }
//        }
//    }


    
//}
