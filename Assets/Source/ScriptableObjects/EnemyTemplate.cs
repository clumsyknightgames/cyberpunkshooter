////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : EnemyTemplate.cs
//   Description :
//      Class used to create new enemies for the game
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Class", menuName = "Enemy/New Enemy")]
public class EnemyTemplate : ScriptableObject
{
    public string enemyName;

    [Header("Movement")]
    public float movementSpeed;

    [Space(10)]
    [Header("Vitals")]
    public int maximumHealth;
    public int maximumEnergy; // default enemies dont use energy
    public int baseDefense;

    [Space(10)]
    [Header("Enemy Info")]
    public float agroRange;
    public float attackDamage;
    public float attackSpeed; // attacks per second
    public float attackRange;
}