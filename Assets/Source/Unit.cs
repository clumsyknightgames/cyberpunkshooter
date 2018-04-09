using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected float movSpeed, sprintModifier;
    protected int curHealth, maxHealth;
    protected int curEnergy, maxEnergy;

    // percent damage is reduced by
    protected const int DEFENSE_CAP = 75;
    protected int defense;

    protected bool isDead;

    void Start ()
    {
        isDead = false;
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

    // Utility functions
    public bool damage(int value)
    {
        float dmg = value * (defense/100);
        setCurHealth(curHealth - Mathf.RoundToInt(dmg));

        return isDead;
    }

    public bool heal(int value)
    {
        if (curHealth < maxHealth && curHealth > 0)
        {
            setCurHealth(curHealth + value);
            return true; // was able to heal player
        }
        else
            return false; // was unable to healer player
    }
}
