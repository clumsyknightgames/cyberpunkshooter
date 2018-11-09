////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Entity.cs
//   Description :
//      Class for all possibly destroyable objects in the game to derrive from
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class Entity : LevelObject
{
    public bool isDestroyed;
    public bool isInvulnerable;

    // buildup of damage and healing
    private float dmgBuildup;
    private float healBuildup;

    public Resource health = new Resource("Health", 100);
    public Resource energy = new Resource("Energy", 100);

    // percent damage is reduced by
    private const int DEFENSE_CAP = 75;
    private int defense_;
    public int defense
    {
        get { return defense_; }
        set
        {
            defense_ = value;

            if (defense_ > DEFENSE_CAP)
                defense_ = DEFENSE_CAP;
        }
    }

    private void Start()
    {
        isDestroyed = false;
        isInvulnerable = true;
    
        dmgBuildup = 0;
        healBuildup = 0;
    }

    /// <summary>
    /// Damage entity based on a given value
    /// </summary>
    /// <param name="value">Damage to apply to entity</param>
    /// <returns>Returns true of the entity runs out of health, false if not</returns>
    public virtual bool damage(float value)
    {
        // make sure the entity is not invulnerable
        if (!isInvulnerable)
        {
            // apply damage mitigation
            float dmgRecieved = value * ((100 - defense) / 100f);
            float dmgTaken = 0;

            // check if the damage was a decimal number
            if (dmgRecieved % 1 != 0) // if there is a remainder
            {
                // place the remainder into the wounds variable
                dmgBuildup += dmgRecieved % 1;

                if (dmgBuildup >= 1) // if the damage has built up to a whole number
                {
                    // add the lowest whole number to the damage
                    dmgTaken += Mathf.FloorToInt(dmgBuildup);
                    // subtract the lowest whole number from the built up damage
                    dmgBuildup -= Mathf.FloorToInt(dmgBuildup);
                }
            }
            // get the whole number of the damage recieved and store in damage
            dmgTaken += Mathf.FloorToInt(dmgRecieved);

            // subtract the damage to be applied from current health
            health.curResource -= Mathf.FloorToInt(dmgTaken);

            Debug.Log("Entity: " + name + " took " + dmgTaken + " damage. Health left: " + health.curResource);

            // if the entity still has health
            if(health.curResource > 0)
            {
                return false; // report back the entity is still alive
            }
            else // entity has no health left
            {
                // destroy the object
                Debug.Log("DED");
                isDestroyed = true;
                kill();
                return true; // report back the entity is dead
            }
        }
        else
            return false; // object is invulernable, report back it is still alive
    }

    /// <summary>
    /// Heal the entity for the given amount
    /// </summary>
    /// <param name="value">Amount we want to heal the entity by</param>
    /// <returns>Returns true if able to heal, false if not</returns>
    public virtual bool heal(float value)
    {
        // make sure current health is less than max health, and greater than zero
        if (health.curResource < health.maxResource && health.curResource > 0)
        {
            dmgBuildup -= value;

            // if the amount we want to heal is above the amount of damage buildup
            if (dmgBuildup < 0)
            {
                // heal buildup is the amount of damage buildup that has gone netagive after adding what we want to heal
                healBuildup += dmgBuildup * -1;
                dmgBuildup = 0;

                // heal the entity for the whole number
                health.curResource += Mathf.FloorToInt(healBuildup);

                // store remainder of heal in the buildup
                healBuildup = healBuildup % 1;
            }
            return true; // was able to heal entity
        }
        else
            return false; // was unable to healer entity
    }

    //public override bool interact() { }

    /// <summary>
    /// called when the entity reaches 0 health
    /// </summary>
    protected virtual void kill()
    {
        Destroy(gameObject);
    }
}
