using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject target;
    public SphereCollider agroSphere;

    public EnemyTemplate e;

    public List<GameObject> possibleTargets = new List<GameObject>();

    private void Start()
    {
        agroSphere.radius = e.agroRange;
    }

    public GameObject findNewTarget()
    {

        // temp to prevent compile errors
        GameObject t = new GameObject();
        return t;
    }

    public void Spawn()
    {
        findNewTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            possibleTargets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        possibleTargets.Remove(other.gameObject);
    }
}
