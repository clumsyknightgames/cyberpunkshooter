////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : EnemyManager.cs
//   Description :
//      Spawn enemies based on what area the player is in
//      keep maximum number of active enemies below a limit and cache locations
//      of enemies that get too far away from player which will then be spawned
//      closer to the player
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // maximum amount of active enemies at one time (linked to difficulty)
    private int maxEnemies;
}
