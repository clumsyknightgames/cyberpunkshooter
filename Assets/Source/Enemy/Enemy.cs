using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject target;
    private float agroRange = 40f;

    public float getAgroRange()
    {
        return agroRange;
    }
    public void setAgroRange(int value)
    {
        agroRange = value;
    }

    public GameObject findNewTarget()
    {
        // loop through GameObject.FindGameObjectsWithTag("Player"); and find the closest player
        // check that the distance is <= agroRange;

        // temp to prevent compile errors
        GameObject t = new GameObject();
        return t;
    }

    public void Spawn()
    {
        findNewTarget();
    }
}
