////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : WeaponTemplate.cs
//   Description :
//      Class used to create new Weapon objects for the game
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Player/Items/New Weapon")]
public class WeaponTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public float damageMultiplier;
    public float weaponRateOfFire;
    public float weaponRange;
    public float reloadTime;
    public List<MagazineTemplate> magazineTypes;

    [Space(10)]
    [Header("Mod Slots")]
    public List<WeaponModSlot> weaponModSlots;
    
    [Space(10)]
    [Header("Weapon Artwork")]
    public GameObject BlendFile;
    public Mesh weaponModel;
    public Mesh barrelModel;
    public Material weaponMaterial;

    [Space(10)]
    [Header("Weapon Sounds")]
    public AudioSource[] weaponShootSounds;
    public AudioSource[] weaponImpactSounds;
}