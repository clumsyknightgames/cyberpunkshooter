using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    //// Methods that can be overridden for more functionality
    //public override bool damage(float value) { }
    //public override bool heal(float value) { }

    public bool isInteractable = false;

    public virtual bool interact()
    {
        if (isInteractable)
        {
            // default interactable code here
            return true; // we were able to interact
        }
        return false; // interact failed
    }
}
