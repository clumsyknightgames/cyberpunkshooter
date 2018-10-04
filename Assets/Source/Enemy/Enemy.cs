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
        if (other.tag == "Player")
        {
            possibleTargets.Add(other.gameObject);
            Debug.Log("Aggro raised by " + other.tag.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        possibleTargets.Remove(other.gameObject);
    }
}
