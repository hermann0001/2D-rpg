using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomTrigger : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;

    public SceneTransition storage;
    //public bool needText;
    //public string placeName;
    //public GameObject text;
    //public Text placeText;


    //private void Start()
    //{
    //    if (storage.needText)
    //        StartCoroutine(placeNameCo());
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            storage.playerInitialValue = playerPosition;
            //storage.lastMove = 
            //storage.needText = needText;
            //storage.placeName = placeName;
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    //private IEnumerator placeNameCo()
    //{
    //    text.SetActive(true);
    //    placeText.text = storage.placeName;
    //    yield return new WaitForSeconds(4f);
    //    text.SetActive(false);
    //} 
}
