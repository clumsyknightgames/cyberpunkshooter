using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private bool hasTarget;
    private GameObject target;
    public SphereCollider aggroSphere;

    public EnemyTemplate enemyType;

    public List<GameObject> possibleTargets = new List<GameObject>();

    private void Start()
    {
        aggroSphere.radius = enemyType.agroRange;
        aggroSphere.gameObject.layer = 2;
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
}
