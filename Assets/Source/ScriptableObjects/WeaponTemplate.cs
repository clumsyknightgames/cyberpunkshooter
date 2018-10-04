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
    public List<WeaponUpgradeTemplate> weaponUpgrades;
    
    [Space(10)]
    [Header("Weapon Artwork")]
    public GameObject BlendFile;
    public Mesh weaponModel;
    public Material weaponMaterial;

    [Space(10)]
    [Header("Weapon Sounds")]
    public AudioSource[] weaponShootSounds;
    public AudioSource[] weaponImpactSounds;
}