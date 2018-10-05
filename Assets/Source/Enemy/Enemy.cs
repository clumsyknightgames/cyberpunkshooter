using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject target;
    public SphereCollider aggroSphere;

    public EnemyTemplate e;

    public List<GameObject> possibleTargets = new List<GameObject>();

    private void Start()
    {
        aggroSphere.radius = e.agroRange;
        aggroSphere.gameObject.layer = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            possibleTargets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        possibleTargets.Remove(other.gameObject);
    }
}
