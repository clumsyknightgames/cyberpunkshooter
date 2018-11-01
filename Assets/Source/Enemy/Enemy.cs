////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Enemy.cs
//   Description :
//      Base enemy object
//
//   Created On: 16/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public bool awake; // is the enemy active
    private bool hasTarget;
    private GameObject target;
    public SphereCollider aggroSphere;

    public EnemyTemplate enemyType;

    public bool canAttack;
    private float atkDamage;
    private float atkSpeed;
    private float atkRange;
    private float agroRange;

    private const float TARGET_SEEK_SPEED = 1;

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
        {
            hasTarget = true;
            InvokeRepeating("attack", TARGET_SEEK_SPEED, TARGET_SEEK_SPEED);
        }
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
            CancelInvoke("attack");
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

        canAttack = true;
        atkDamage = enemyType.attackDamage;
        atkSpeed = enemyType.attackSpeed;
        atkRange = enemyType.attackRange;

        agroRange = enemyType.agroRange;
        aggroSphere.radius = agroRange;
        aggroSphere.gameObject.layer = 2;
    }

    /// <summary>
    /// If the enemy has not already attacked find the closest target and 
    /// attack if it's in range
    /// </summary>
    protected virtual void attack()
    {
        if(canAttack)
        {
            GameObject target = getClosestTarget();
            if(target != null)
            {
                // check if target is in attack range
                if (Vector3.Distance(target.transform.position, transform.position) <= atkRange)
                {
                    canAttack = false;

                    if (target.GetComponent<Entity>())
                    {
                        Debug.Log("Enemy attacked for " + atkDamage);

                        // check if we killed the target, if so remove it from the list of targets
                        if(target.GetComponent<Entity>().damage(atkDamage))
                            removeTarget(target);
                    }
                    Invoke("attackCooldown", atkSpeed);
                }
            }
        }
    }
    protected void attackCooldown()
    {
        canAttack = true;
    }

    /// <summary>
    /// Go through the list of possible targets and return the closest one
    /// </summary>
    /// <returns>The closest target as a GameObject</returns>
    protected GameObject getClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDist = -1;

        foreach (GameObject t in possibleTargets)
        {
            float dist = Vector3.Distance(t.transform.position, transform.position);
            if (closestDist == -1 || dist < closestDist)
            {
                closestDist = dist;
                closestTarget = t;
            }

        }

        return closestTarget;
    }

    private void OnDestroy()
    {
        // clear any invokes that may be running on this enemy
        CancelInvoke();
    }
}
