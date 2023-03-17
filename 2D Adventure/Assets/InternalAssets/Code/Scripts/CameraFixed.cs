using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{ 
    public Vector2 position;

    private void Start()
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
