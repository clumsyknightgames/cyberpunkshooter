using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Magazine", menuName = "Player/Items/New Magazine")]
public class magazineTemplate : ScriptableObject
{
    public ammoTemplate ammoType;
    public int ammoCount;
    public float reloadMultiplier = 1;

    [Space(10)]
    [Header("Artwork")]
    public Mesh magazineModel;
    public Material magazineMaterial;

    [Space(10)]
    [Header("Sounds")]
    public AudioSource[] weaponShootSounds;
    public AudioSource[] weaponImpactSounds;
}