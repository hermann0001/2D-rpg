using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TouchControllerHandler : MonoBehaviour     //animation handler
{
    private Animator touchController;

    private void Start()
    {
        touchController = GetComponent<Animator>();
    }
    public void Pressed(string direction)
    {
        touchController.SetBool(direction, true);
    }

    public void Released(string direction)
    {
        touchController.SetBool(direction, false);
    }

}
