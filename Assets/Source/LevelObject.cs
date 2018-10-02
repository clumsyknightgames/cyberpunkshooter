using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : Entity
{
    protected int curHealth, maxHealth;
    protected float wounds;
    // percent damage is reduced by
    protected const int DEFENSE_CAP = 75;
    protected int defense;
   
    protected bool isDestroyed;
    protected bool isInteractable;
    protected bool isDestructable;

    void Start ()
    {
        isDestroyed = false;
	}

    #region getters and setters
    public int getCurHealth()
    {
        return curHealth;
    }
    public void setCurHealth(int value)
    {
        curHealth = value;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        else if (curHealth < 0)
        {
            curHealth = 0;
            isDestroyed = true;
        }
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public void setMaxHealth(int value)
    {
        maxHealth = value;
    }
    public int getDefense()
    {
        return defense;
    }
    public void setDefense(int value)
    {
        defense = value;

        if (defense > DEFENSE_CAP)
            defense = DEFENSE_CAP;
    }
    #endregion

    public override bool damage(float value)
    {
        if (isDestructable)
        {
            float dmg = value * (defense / 100);

            // get the whole number of the damage recieved and store in damage
            if (dmg % 1 > 0) // if there is a remainder
            {
                wounds += dmg % 1; // place the remainder into the wounds variable

                if (wounds >= 1) // if the damage has built up to a whole number
                {
                    dmg += Mathf.FloorToInt(wounds); // add the lowest whole number to the damage
                    wounds -= Mathf.FloorToInt(wounds); // subtract the lowest whole number from the built up damage
                }
            }

            setCurHealth(Mathf.FloorToInt(dmg));

            return isDestroyed;
        }
        else
            return false;
    }
    public override bool heal(float value)
    {
        if (curHealth < maxHealth && curHealth > 0)
        {
            wounds -= value % 1;
            if (wounds <= -1)
            {
                setCurHealth(curHealth + Mathf.FloorToInt(-1 * wounds));
                wounds += -1 * Mathf.FloorToInt(wounds);
            }
            setCurHealth(curHealth + Mathf.FloorToInt(value));
            return true; // was able to heal player
        }
        else
            return false; // was unable to healer player
    }

    public override bool interactable()
    {
        return isInteractable;
    }
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
