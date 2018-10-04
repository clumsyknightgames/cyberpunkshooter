using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Magazine", menuName = "Player/Items/New Magazine")]
public class MagazineTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public AmmoTemplate ammoType;
    public int ammoCount;
    public float reloadMultiplier = 1;

    [Space(10)]
    [Header("Artwork")]
    public Mesh magazineModel;
    public Material magazineMaterial;

    [Space(10)]
    [Header("Sounds")]
    public AudioSource[] magazineReloadSounds;
    public AudioSource[] magazineDropSounds;
}