using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    [Space(10)]
    [Header("Weapon Artwork")]
    public GameObject BlendFile;
    public Mesh weaponModel;
    public Material weaponMaterial;

    [Space(10)]
    [Header("Weapon Sound Overrides")]
    public AudioSource[] weaponShootSounds;
    public AudioSource[] weaponImpactSounds;
}