////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : InteractTrigger.cs
//   Description :
//      Used for buttons to allow them to trigger a specified object
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class InteractTrigger : LevelObject
{
    // The object this trigger will interact with
    public GameObject TargetObject;
    private LevelObject targetSource;

    private void Start()
    {
        if(TargetObject.GetComponent<LevelObject>())
            targetSource = TargetObject.GetComponent<LevelObject>();

    }

    public void use()
    {
        bool interacted = targetSource.interact();
    }
}
