using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="New Ammo Type", menuName = "Player/Items/New Ammo Type")]
public class AmmoTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public float Damage;
   
    [Space(10)]
    [Header("Brass Artwork")]
    public Mesh brassModel;
    public Material brassMaterial;

    [Space(10)]
    [Header("Sounds")]
    public AudioSource[] brassImpactSounds;
    public AudioSource[] bulletImpactSounds;
}