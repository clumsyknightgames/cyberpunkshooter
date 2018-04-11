using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    protected int curHealth, maxHealth;
    // percent damage is reduced by
    protected const int DEFENSE_CAP = 75;
    protected int defense;

    protected bool isDestroyed;
    protected bool isInteractable;
    protected bool isDestructable;

    // Use this for initialization
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

    public bool damage(int value)
    {
        if (isDestructable)
        {
            float dmg = value * (defense / 100);
            setCurHealth(curHealth - Mathf.RoundToInt(dmg));

            return isDestroyed;
        }
        else
            return false;
    }
    public bool interactable()
    {
        return isInteractable;
    }
    public bool interact()
    {
        if(isInteractable)
        {
            // interactable code here
            return true; // we were able to interact
        }
        return false; // interact failed
    }
}
