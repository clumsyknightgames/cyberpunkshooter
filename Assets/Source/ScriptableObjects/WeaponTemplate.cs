using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Player/Items/New Weapon")]
public class WeaponTemplate : ScriptableObject
{
    public DamageRange WeaponDamageRange;
    public float WeaponRateOfFire;
    public float weaponRange;

    [Space(10)]
    [Header("Weapon Artwork")]
    public Mesh WeaponModel;
    public Material weaponMaterial;
}

[System.Serializable]
public struct DamageRange
{
    public float minDamage;
    public float maxDamage;
}