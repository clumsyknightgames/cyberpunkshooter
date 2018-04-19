using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // store 'sockets' for Muzzle, Barrel, trigger and ejection area

    protected int currentAmmo;

    private GameObject muzzle;
    private GameObject ejectionPort;
    private GameObject magwell;
    public GameObject Brass;

    private PlayerController player;

    private PlayerTemplate playerData;
    private WeaponTemplate weaponData;
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
        ejectionPort = transform.GetChild(1).gameObject;
        magwell = transform.GetChild(1).gameObject;
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
                // Insert reload timer here
                currentAmmo = MagToReload.ammoCount;
                ammoData = MagToReload.ammoType;
                player.Magazines[MagToReload]--;
                Debug.Log("Reloaded weapon");
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

                Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * 100, Color.green, 10); // Show shot in green
                if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, weaponRange))
                {
                    // Show shot travel from muzzle to impact as red line
                    Debug.DrawLine(muzzle.transform.position, hit.point, Color.red, 10);
                    Debug.Log("HIT: " + hit.collider.gameObject);
                }
                Debug.Log("Pew.");

                // Brass ejection
                // Create new instance, set parameters according to currently loaded ammo
                GameObject newBrass = Instantiate(Brass);
                newBrass.GetComponent<MeshFilter>().mesh = ammoData.brassModel;
                newBrass.GetComponent<MeshRenderer>().material = ammoData.brassMaterial;
                newBrass.GetComponent<MeshCollider>().sharedMesh = ammoData.brassModel;

                // Move brass to ejection location and align with barrel
                newBrass.transform.position = ejectionPort.transform.position;
                newBrass.transform.rotation = muzzle.transform.rotation;

                // Set ejection force with slight randomisation
                Vector3 ejectionVector = new Vector3();
                ejectionVector.x = Random.Range(-1f, 1f);
                ejectionVector.y = Random.Range(-0.5f, 0.5f);
                ejectionVector.z = 5 + Random.Range(-0.5f, 0.5f);

                // Set ejection torque with slight randomisation
                Vector3 ejectionTorque = new Vector3(Random.Range(-3f, -5f), Random.Range(-3f, -5f), Random.Range(3f, 5f));

                // Apply force and torque
                newBrass.GetComponent<Rigidbody>().velocity = ejectionPort.transform.TransformDirection(ejectionVector);
                newBrass.GetComponent<Rigidbody>().AddTorque(ejectionTorque);
                
                Debug.DrawRay(ejectionPort.transform.position, ejectionPort.transform.TransformDirection(ejectionVector), Color.blue, 0.1f);
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
        MagazineTypes = weaponData.magazineTypes;
        rateOfFire = weaponData.weaponRateOfFire;
        // wepDamageRange = wep.weaponDamageRange;
        weaponRange = weaponData.weaponRange;

        gameObject.GetComponent<MeshFilter>().mesh = weaponData.weaponModel;
        gameObject.GetComponent<Renderer>().material = weaponData.weaponMaterial;

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
