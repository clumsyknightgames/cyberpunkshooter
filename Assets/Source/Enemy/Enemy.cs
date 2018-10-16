using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private bool hasTarget;
    private GameObject target;
    public SphereCollider aggroSphere;

    public EnemyTemplate enemyType;

    public bool canAttack;
    private float atkDamage;
    private float atkSpeed;
    private float atkRange;

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

        canAttack = true;
        atkDamage = enemyType.attackDamage;
        atkSpeed = enemyType.attackSpeed;
        atkRange = enemyType.attackRange;

        aggroSphere.radius = enemyType.agroRange;
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
                    StartCoroutine("AttackCooldown");
                }
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(atkSpeed);
        canAttack = true;
        StopCoroutine("AttackCooldown");
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
}
