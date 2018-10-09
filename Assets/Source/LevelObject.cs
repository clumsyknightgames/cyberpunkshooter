using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : Entity
{
    protected int curHealth_;
    public int curHealth
    {
        get { return curHealth_; }
        set
        {
            curHealth_ = value;

            if (curHealth_ > maxHealth_)
                curHealth_ = maxHealth_;
            else if (curHealth_ < 0)
            {
                curHealth_ = 0;
                isDestroyed = true;
            }
        }
    }
    protected int maxHealth_;
    public int maxHealth { get { return maxHealth_; } set { maxHealth_ = value; } }

    // buildup of damage and healing
    protected float dmgBuildup;
    protected float healBuildup;

    // percent damage is reduced by
    protected const int DEFENSE_CAP = 75;
    protected int defense_;
    public int defense
    {
        get { return defense_; }
        set
        {
            defense = value;

            if (defense > DEFENSE_CAP)
                defense = DEFENSE_CAP;
        }
    }
   
    protected bool isDestroyed = false;
    protected bool isInteractable = false;
    protected bool isDestructable = true;

    /// <summary>
    /// Damage object based on a given value
    /// </summary>
    /// <param name="value">Damage to apply to object</param>
    /// <returns>Returns true of the object was destroyed, false if not</returns>
    public override bool damage(float value)
    {
        if (isDestructable)
        {
            float dmg = value * (defense / 100);

            // get the whole number of the damage recieved and store in damage
            if (dmg % 1 != 0) // if there is a remainder
            { 
                // place the remainder into the wounds variable
                dmgBuildup += dmg % 1;

                if (dmgBuildup >= 1) // if the damage has built up to a whole number
                { 
                    // add the lowest whole number to the damage
                    dmg += Mathf.FloorToInt(dmgBuildup);
                    // subtract the lowest whole number from the built up damage
                    dmgBuildup -= Mathf.FloorToInt(dmgBuildup); 
                }
            }

            // subtract the damage to be applied from current health
            curHealth -= Mathf.FloorToInt(dmg);

            return isDestroyed;
        }
        else
            return false;
    }

    /// <summary>
    /// Heal the object for the given amount
    /// </summary>
    /// <param name="value">Amount we want to heal the object by</param>
    /// <returns>Returns true if able to heal, false if not</returns>
    public override bool heal(float value)
    {
        // make sure current health is less than max health, and greater than zero
        if (curHealth < maxHealth && curHealth > 0)
        {
            dmgBuildup -= value;

            // if the amount we want to heal is above the amount of damage buildup
            if(dmgBuildup < 0)
            {
                // heal buildup is the amount of damage buildup that has gone netagive after adding what we want to heal
                healBuildup += dmgBuildup * -1;
                dmgBuildup = 0;

                // heal the object for the whole number
                curHealth += Mathf.FloorToInt(healBuildup);

                // store remainder of heal in the buildup
                healBuildup = healBuildup % 1;
            }
            return true; // was able to heal object
        }
        else
            return false; // was unable to healer object
    }

    public override bool interactable() { return isInteractable; }

    public override bool interact()
    {
        if(isInteractable)
        {
            // default interactable code here
            return true; // we were able to interact
        }
        return false; // interact failed
    }
}
