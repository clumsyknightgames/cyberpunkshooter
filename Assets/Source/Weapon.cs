﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // store 'sockets' for Muzzle, Barrel, trigger and ejection area

    protected int currentAmmo;

    private GameObject muzzle;
    public GameObject BrassPrefab;
    public GameObject MagazinePrefab;
    private GameObject gunModel;
    private Transform ejectionPort;
    private Transform ejectionDirection;
    private Transform magazineWell;

    private PlayerController player;

    private PlayerTemplate playerData;
    private WeaponTemplate weaponData;
    private magazineTemplate loadedMag;
    private ammoTemplate ammoData;

    public List<magazineTemplate> MagazineTypes;

    private float rateOfFire;
    private float fireTimer;
    private float weaponRange;

    // might switch to arrays instead? undecoded
    public List<AudioSource> shootSounds = new List<AudioSource>();
    public List<AudioSource> impactSounds = new List<AudioSource>();

    /*#region getters and setters
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
    #endregion*/
    private void Start()
    {
        muzzle = transform.GetChild(0).gameObject;
        player = transform.parent.gameObject.GetComponent<PlayerController>();
        currentAmmo = 0;
    }

    public void Reload()
    {
        if (CanReload())
        {
            magazineTemplate MagToReload = GetFirstMagType();
            if (player.Magazines[MagToReload] > 0)
            {
                // Detach old magazine
                Transform oldMag;
                if (oldMag = transform.Find("Magazine"))
                {
                    Debug.Log("Ejecting old mag.");
                    oldMag.gameObject.AddComponent<Rigidbody>();
                    oldMag.gameObject.GetComponent<Rigidbody>().mass = 0.1f;
                    oldMag.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    oldMag.parent = transform.parent.parent;
                    oldMag.position = new Vector3(oldMag.position.x, oldMag.position.y - 0.05f, oldMag.position.z);
                }

                // Refill ammo
                currentAmmo = MagToReload.ammoCount;
                ammoData = MagToReload.ammoType;
                player.Magazines[MagToReload]--;
                loadedMag = MagToReload;

                // Attach a magazine to the gun
                GameObject newMag = Instantiate(MagazinePrefab, transform.position, Quaternion.identity) as GameObject;
                newMag.transform.name = "Magazine";
                newMag.transform.SetParent(transform);
                if (magazineWell = gunModel.transform.Find("Magazine"))
                {
                    newMag.transform.position = transform.TransformPoint(magazineWell.position);
                }
                newMag.transform.rotation = transform.rotation;

                Debug.Log("Reloaded " + weaponData.Name + " with " + MagToReload.Name + ".");
            }
        }
    }

    public magazineTemplate GetFirstMagType()
    {
        foreach (magazineTemplate magazineType in MagazineTypes)
        {
            if (player.Magazines.ContainsKey(magazineType))
            {
                if (player.Magazines[magazineType] > 0)
                {
                    return magazineType;
                }
            }
        }
        return null;
    }

    public bool CanReload()
    {
        if (GetFirstMagType() != null)
        {
            return true;
        }
        Debug.Log("No magazines!");
        return false;
    }

    public void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= rateOfFire)
        {
            //Debug.Log("Ready to fire after " + fireTimer + "s.");
            if (currentAmmo > 0)
            {
                currentAmmo--;
                RaycastHit hit;

                // Show shot in green
                Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * 100, Color.green, 10);
                if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, weaponRange))
                {
                    // Show shot travel from muzzle to impact as red line
                    Debug.DrawLine(muzzle.transform.position, hit.point, Color.red, 10);
                    Debug.Log("HIT: " + hit.collider.gameObject);
                }
                Debug.Log("Pew.");

                #region Brass ejection
                // Create new instance, set parameters according to currently loaded ammo
                GameObject newBrass = Instantiate(BrassPrefab);
                newBrass.GetComponent<MeshFilter>().mesh = ammoData.brassModel;
                newBrass.GetComponent<MeshRenderer>().material = ammoData.brassMaterial;
                newBrass.GetComponent<MeshCollider>().sharedMesh = ammoData.brassModel;

                // Move brass to ejection location and align with barrel
                newBrass.transform.position = transform.TransformPoint(ejectionPort.position);
                newBrass.transform.rotation = muzzle.transform.rotation;

                // Set ejection force with slight randomisation
                Vector3 worldEjectionVector = transform.TransformPoint(ejectionDirection.position) - transform.TransformPoint(ejectionPort.position);
                Vector3 ejectionVector = (ejectionDirection.position - ejectionPort.position).normalized;
                ejectionVector.x = ejectionVector.x + Random.Range(-0.3f, 0.6f);
                ejectionVector.y = ejectionVector.y + Random.Range(-0.3f, 0.6f);
                ejectionVector.z = ejectionVector.z + Random.Range(-0.3f, 0.6f);
                worldEjectionVector = transform.TransformVector(ejectionVector);

                // Apply force
                newBrass.GetComponent<Rigidbody>().AddForceAtPosition(worldEjectionVector, ejectionPort.TransformPoint(new Vector3(0, 0.05f, 0)));

                // Draw a debug ray to show ejection vector
                Debug.DrawRay(newBrass.transform.TransformPoint(new Vector3(0, 0.05f, 0)), worldEjectionVector, Color.blue, 0.1f);
                #endregion
            }
            else
            {
                Debug.Log("Magazine empty");
            }
            fireTimer = 0;
        }
    }

    public void equipWeapon(WeaponTemplate wep)
    {
        weaponData = wep;
        gunModel = weaponData.BlendFile;
        ejectionPort = gunModel.transform.Find("EjectionPort");
        ejectionDirection = gunModel.transform.Find("EjectionDirection");
        MagazineTypes = weaponData.magazineTypes;
        rateOfFire = weaponData.weaponRateOfFire;
        // wepDamageRange = wep.weaponDamageRange;
        weaponRange = weaponData.weaponRange;

        gameObject.GetComponent<MeshFilter>().mesh = weaponData.weaponModel;
        gameObject.GetComponent<Renderer>().material = weaponData.weaponMaterial;

        Debug.Log(weaponData.Name + " equipped.");

        /*foreach(AudioSource s in wep.weaponShootSounds)
        {
            shootSounds.Add(s);
        }
        foreach (AudioSource s in wep.weaponImpactSounds)
        {
            impactSounds.Add(s);
        }*/
    }
}
