using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Player/Items/New Weapon")]
public class WeaponTemplate : ScriptableObject
{
    public DamageRange weaponDamageRange;
    public float weaponRateOfFire;
    public float weaponRange;

    [Space(10)]
    [Header("Weapon Artwork")]
    public Mesh weaponModel;
    public Material weaponMaterial;

    [Space(10)]
    [Header("Weapon Sounds")]
    public AudioSource[] weaponShootSounds;
    public AudioSource[] weaponImpactSounds;
}

[System.Serializable]
public struct DamageRange
{
    public float minDamage;
    public float maxDamage;
}