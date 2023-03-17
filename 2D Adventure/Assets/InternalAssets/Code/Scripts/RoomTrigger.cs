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
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
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
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    public IEnumerator FadeCo()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
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
