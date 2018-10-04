using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : Entity
{
    protected float movSpeed, sprintModifier;
    protected int curHealth, maxHealth;
    protected float wounds; // remainder of damage after applying whole number to damage
    protected int curEnergy, maxEnergy;

    // percent damage is reduced by
    protected int DEFENSE_CAP = 75;
    protected int defense;

    protected bool isDead;
    protected bool isInteractable;

    void Start ()
    {
        isDead = false;
        wounds = 0f;
    }

    #region getters and setters
    public float getMovSpeed()
    {
        return movSpeed;
    }
    public void setMovSpeed(float value)
    {
        movSpeed = value;
    }

    public float getSprintModifier()
    {
        return sprintModifier;
    }
    public void setSprintModifier(float value)
    {
        sprintModifier = value;
    }

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
            isDead = true;
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

    public int getCurEnergy()
    {
        return curEnergy;
    }
    public void setCurEnergy(int value)
    {
        curEnergy = value;
        if (curEnergy > maxEnergy)
            curEnergy = maxEnergy;
    }
    public int getMaxEnergy()
    {
        return maxEnergy;
    }
    public void setMaxEnergy(int value)
    {
        maxEnergy = value;
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
        float dmg = value * (defense/100);

        // get the whole number of the damage recieved and store in damage
        if(dmg % 1 > 0) // if there is a remainder
        {
            wounds += dmg % 1; // place the remainder into the wounds variable

            if (wounds >= 1) // if the damage has built up to a whole number
            {
                dmg += Mathf.FloorToInt(wounds); // add the lowest whole number to the damage
                wounds -= Mathf.FloorToInt(wounds); // subtract the lowest whole number from the built up damage
            }
        }

        setCurHealth(Mathf.FloorToInt(dmg));
        return isDead;
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
        if (isInteractable)
        {
            // default interactable code here
            return true; // we were able to interact
        }
        return false; // interact failed
    }
}
