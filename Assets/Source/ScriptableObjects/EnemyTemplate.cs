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