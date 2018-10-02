using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract bool damage(float value);
    public abstract bool heal(float value);

    public abstract bool interactable();
    public abstract bool interact();
}
