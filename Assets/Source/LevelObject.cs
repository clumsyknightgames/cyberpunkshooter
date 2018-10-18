////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Unit.cs
//   Description :
//      Base class for any object in the game that needs interaction
//      to derrive from
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
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
