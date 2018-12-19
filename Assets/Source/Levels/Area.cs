////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Zone.cs
//   Description :
//      Each area or room of the level is a zone and may contain information
//      to allow the enemy manager to spawn enemies inside of it
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    // Area information
    public int areaID { get; set; }
    public string areaName { get; set; }

    // number of enemies inside the zone
    public int numEnemies { get; set; }

    // allows for empty gameobjects to be added onto this list to create new places where enemies can possibly spawn
    private List<GameObject> enemySpawn = new List<GameObject>();

    /// <summary>
    /// Grab a random spawn location from a list of possible spawns for the area
    /// </summary>
    /// <returns>A vector3 containing the position to spawn the enemy at</returns>
    public Vector3 getSpawnLocation()
    {
        // check to make sure there is a possible spawner to use
        if(enemySpawn.Count <= 0)
        {
            Debug.LogError("Area: " + "ID:" + areaID + ", " + areaName + " Has no avaliable spawners.");
            return new Vector3(0, 0, 0);
        }
        else
        {
            // pick a random spawner from the list of spawners and get the location of it
            return enemySpawn[Random.Range(0, enemySpawn.Count)].transform.position;
        }
    }
}
