////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Player.cs
//   Description :
//      Class used to create new players for the game
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
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