using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ammo Type", menuName = "Player/Items/New Ammo Type")]
public class ammoTemplate : ScriptableObject
{
    public string ammoName;
    public DamageRange DamageRange;
   
    [Space(10)]
    [Header("Brass Artwork")]
    public Mesh brassModel;
    public Material brassMaterial;

    [Space(10)]
    [Header("Sounds")]
    public AudioSource[] brassImpactSounds;
    public AudioSource[] bulletImpactSounds;
}

[System.Serializable]
public struct DamageRange
{
    public float minDamage;
    public float maxDamage;
}