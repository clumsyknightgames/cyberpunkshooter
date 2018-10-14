using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private bool hasTarget;
    private GameObject target;
    public SphereCollider aggroSphere;

    public EnemyTemplate enemyType;
    private float atkDamage;
    private float atkSpeed;

    public List<GameObject> possibleTargets = new List<GameObject>();

    private void Start()
    {
        initializeEnemy();
    }

    /// <summary>
    /// Add a gameobject to the list of possible targets
    /// </summary>
    /// <param name="t">The target to add</param>
    public void addTarget(GameObject t)
    {
        possibleTargets.Add(t);

        if (!hasTarget)
            hasTarget = true;
    }
    /// <summary>
    /// Remove a gameobject from the list of possible targets
    /// </summary>
    /// <param name="t">The target to remove</param>
    public void removeTarget(GameObject t)
    {
        possibleTargets.Remove(t);

        if (possibleTargets.Count <= 0)
        {
            hasTarget = false;
        }
    }

    private void initializeEnemy()
    {
        name = enemyType.enemyName;

        movSpeed = enemyType.movementSpeed;

        health.maxResource = enemyType.maximumHealth;
        health.curResource = health.maxResource;

        energy.maxResource = enemyType.maximumEnergy;
        energy.curResource = energy.maxResource;

        defense = enemyType.baseDefense;

        atkDamage = enemyType.attackDamage;
        atkSpeed = enemyType.attackSpeed;

        aggroSphere.radius = enemyType.agroRange;
        aggroSphere.gameObject.layer = 2;
    }
}
