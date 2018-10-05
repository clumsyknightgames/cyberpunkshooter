using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Mod Slot", menuName = "Player/Items/New Weapon Mod Slot")]
public class WeaponModSlot : ScriptableObject
{
    public string Name;
    public string Description;

    public List<WeaponUpgradeTemplate> AllowedMods;
}