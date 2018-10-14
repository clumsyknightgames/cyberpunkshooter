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
    public int maximumEnergy;
    public int baseDefense;

    [Space(10)]
    [Header("Inventory")]
    public List<WeaponTemplate> weapons;
    public List<MagazineSlot> magazines;
}

[System.Serializable]
public struct MagazineSlot
{
    public MagazineTemplate MagazineType;
    public int MagazineCount;
}