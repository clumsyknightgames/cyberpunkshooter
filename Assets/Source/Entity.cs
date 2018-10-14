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
    /// Damage object based on a given value
    /// </summary>
    /// <param name="value">Damage to apply to object</param>
    /// <returns>Returns true of the object was destroyed, false if not</returns>
    public virtual bool damage(float value)
    {
        if (!isInvulnerable)
        {
            float dmgRecieved = 9.8f;
            float dmgTaken = 0;
            Debug.Log(value + " * ((100 - " + defense + ") / 100) = " + value * ((100 - defense) / 100));

            // get the whole number of the damage recieved and store in damage
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
            dmgTaken += Mathf.FloorToInt(dmgRecieved);
            // subtract the damage to be applied from current health
            health.curResource -= Mathf.FloorToInt(dmgTaken);

            Debug.Log("Entity: " + name + " took " + dmgTaken + " damage. Health left: " + health.curResource);

            if(health.curResource > 0)
            {
                return false;
            }
            else
            {
                Debug.Log("DED");
                isDestroyed = true;
                kill();
                return true;
            }
        }
        else
            return false;
    }

    /// <summary>
    /// Heal the object for the given amount
    /// </summary>
    /// <param name="value">Amount we want to heal the object by</param>
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

                // heal the object for the whole number
                health.curResource += Mathf.FloorToInt(healBuildup);

                // store remainder of heal in the buildup
                healBuildup = healBuildup % 1;
            }
            return true; // was able to heal object
        }
        else
            return false; // was unable to healer object
    }

    //public override bool interact() { }

    /// <summary>
    /// called when the object reaches 0 health
    /// </summary>
    private void kill()
    {
        Destroy(this);
    }
}
