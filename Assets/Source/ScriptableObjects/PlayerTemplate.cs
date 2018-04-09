using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Class", menuName = "Player/New Player Class")]
public class PlayerTemplate : ScriptableObject
{
    public string className;

    [Header("Movement")]
    public float movementSpeed;
    public float sprintSpeedModifier;

    [Space(10)]
    [Header("Vitals")]
    public int maximumHealth;
    [Space(3)]
    public int maximumEnergy;
    [Space(3)]
    public int baseDefense;

}
