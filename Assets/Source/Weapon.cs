using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // store 'sockets' for Muzzle, Barrel, trigger and ejection area

    protected int magAmmo, numMags;
    protected int magCapacity, maxMags;

    private GameObject muzzle;

    private float rateOfFire;
    private float fireTimer;
    private float weaponRange;

    private DamageRange wepDamageRange;


    // might switch to arrays instead? undecoded
    public List<AudioSource> shootSounds = new List<AudioSource>();
    public List<AudioSource> impactSounds = new List<AudioSource>();

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
    private void Start()
    {
        muzzle = transform.GetChild(0).gameObject;
        fireTimer = 0;

        //TESTING VARS
        magAmmo = 5;
        magCapacity = 60;
        numMags = 1;
        maxMags = 30;
    }

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

    public void fire(Vector3 aimPoint)
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= rateOfFire)
        {
            if(magAmmo > 0)
            {
                setMagAmmo(magAmmo - 1);
                Vector3 pos = muzzle.transform.forward*weaponRange;
                RaycastHit hit;
                pos.y += aimPoint.y;

                Debug.DrawRay(muzzle.transform.position, pos, Color.green, 10); // Show shot in green
                if(Physics.Raycast(muzzle.transform.position, pos, out hit, weaponRange))
                {
					// Show shot travel from muzzle to impact as red line
					Debug.DrawLine(muzzle.transform.position, hit.point, Color.red, 10);
                    Debug.Log("HIT: " + hit.collider.gameObject);
                }
            }
            else
            {
                if (!reload()) // if the player is unable to reload
                    Debug.Log("NO MAGS");

            }
            fireTimer = 0;
        }
    }

    public void equipWeapon(WeaponTemplate wep)
    {
        rateOfFire = wep.weaponRateOfFire;
        wepDamageRange = wep.weaponDamageRange;
        weaponRange = wep.weaponRange;

        gameObject.GetComponent<MeshFilter>().mesh = wep.weaponModel;
        gameObject.GetComponent<Renderer>().material = wep.weaponMaterial;

        foreach(AudioSource s in wep.weaponShootSounds)
        {
            shootSounds.Add(s);
        }
        foreach (AudioSource s in wep.weaponImpactSounds)
        {
            impactSounds.Add(s);
        }
    }

    public bool pickupMagazine(int amount)
    {
        if(numMags >= maxMags)
        {
            return false; // player has maximum amount of magazines
        }
        else
        {
            setNumMags(numMags += amount);
            return true;
        }
    }
}
