using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // store 'sockets' for Muzzle, Barrel, trigger and ejection area

    protected int magAmmo, numMags;
    protected int magCapacity, maxMags;

    #region getters and setters
    public int getMagAmmo()
    {
        return magAmmo;
    }
    public void setMagAmmo(int value)
    {
        if (value > magCapacity)
            magAmmo = magCapacity;
        else
            magAmmo = value;
    }

    public int getNumMags()
    {
        return numMags;
    }
    public void setNumMags(int value)
    {
        if (value > maxMags)
            numMags = maxMags;
        else
            numMags = value;
    }
    #endregion

    public bool reload()
    {
        if (numMags > 0)
        {
            setNumMags(numMags-1); // subtract 1 mag
            setMagAmmo(magCapacity); // reset current mag ammo to mag capacity. partial mags discarded (maybe keep in future? undecided)
            return true; // was able to reload
        }
        else
            return false; // no ammo, unable to reload
    }
    public void fire()
    {
        // fire gun logic
    }
}
