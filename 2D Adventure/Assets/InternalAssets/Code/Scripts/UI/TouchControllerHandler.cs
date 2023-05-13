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
        AudioManager.instance.Play("FootstepSound");
        touchController.SetBool(direction, true);
    }

    public void Released(string direction)
    {
        AudioManager.instance.Stop("FootstepSound");
        touchController.SetBool(direction, false);
    }

}
