////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : AgroTrigger.cs
//   Description :
//      Place on the child trigger collider for the enemy, 
//      Adds or removes a player object to the list of possible targets 
//
//   Created On: 14/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class AgroTrigger : MonoBehaviour
{
    public Enemy enemyObject;

    private void Awake()
    {
        enemyObject = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyObject.addTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyObject.removeTarget(other.gameObject);
    }
}
