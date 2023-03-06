using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTrigger : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            TransitionRoom(other);
    }

    private void TransitionRoom(Collider2D other)
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
