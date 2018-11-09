////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : PlayerController.cs
//   Description :
//      Handle player input, movement and aiming
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Unit
{
    private float sensitivity;
    private Vector3 aimPoint;

    private const float AIM_OFFSET = 1.25f;
    private const float GRAVITY = 250f; // make global variable across all objects in future (possibly)
    private const float USE_RANGE = 3f;

    private int EquippedWeapon = 0;

    // scriptable object skills common methods: cast, canCast (checks if on cooldown, checks if enough energy)

    // Weapon variables
    public List<WeaponTemplate> Weapons;
    public Dictionary<MagazineTemplate, int> Magazines;

    private GameObject weaponObject;
    private Weapon weaponController;

    public GameObject crosshair;

    private GameObject laser;
    private LineRenderer laserLine;

    public PlayerTemplate playerClass;
    private CharacterController controller;

    private Dictionary<string, bool> handledKeys;

    void Start()
    {
        initializePlayerClass();

        handledKeys = new Dictionary<string, bool>();

        weaponObject = transform.GetChild(0).gameObject;
        weaponController = weaponObject.GetComponent<Weapon>();

        controller = GetComponent<CharacterController>();

        Magazines = new Dictionary<MagazineTemplate, int>();

        foreach (MagazineSlot pair in playerClass.magazines)
        {
            Magazines.Add(pair.MagazineType, pair.MagazineCount);
        }
        Weapons = playerClass.weapons;

        sensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 8f);

        EquipWeapon();

        laser = transform.GetChild(1).gameObject;
        laser.transform.position = weaponObject.transform.GetChild(0).position;
        laserLine = laser.GetComponent<LineRenderer>();

        Cursor.visible = false;
    }

    void Update()
    {
        manageInput();
    }
    void FixedUpdate()
    {
        movement();
        aimAtMouse();
    }

    private void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit " + hit.transform.name);
        }
    }

    /// <summary>
    /// Check for input events
    /// </summary>
    private void manageInput()
    {
        if (Input.GetButton("Fire"))
        {
            weaponController.Fire();
        }
        if (Input.GetButton("CastRay"))
        {
            CastRay();
        }

        if (Input.GetButton("Use") && !handledKeys["Use"])
        {
            use();
            handledKeys["Use"] = true;
        }
        else if (!Input.GetButton("Use"))
        {
            handledKeys["Use"] = false;
        }

        if (Input.GetButton("SwapWeapon") && !handledKeys["SwapWeapon"])
        {
            EquipNextWeapon();
            handledKeys["SwapWeapon"] = true;
        }
        else if (!Input.GetButton("SwapWeapon"))
        {
            handledKeys["SwapWeapon"] = false;
        }

        if (Input.GetButton("Reload") && !handledKeys["Reload"])
        {
            weaponController.Reload();
            handledKeys["Reload"] = true;
        }
        else if (!Input.GetButton("Reload"))
        {
            handledKeys["Reload"] = false;
        }


    }

    /// <summary>
    /// Aim towards mouse
    /// </summary>
    private void aimAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            aimPoint.x = hit.point.x;
            aimPoint.y = hit.point.y;
            aimPoint.z = hit.point.z;

            // Yellow line for input-based aim before any corrections
            Debug.DrawLine(weaponObject.transform.GetChild(0).transform.position, aimPoint, Color.yellow, 0);

            // Correct aim for shooting up ramps
            if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("ground")) && ((hit.point.y) > weaponObject.transform.GetChild(0).transform.position.y))
                aimPoint.y = hit.point.y + AIM_OFFSET * hit.normal.y * Mathf.Clamp(hit.point.y - weaponObject.transform.GetChild(0).transform.position.y, 0, 1);

            // Rotate player towards new aim point
            Quaternion targetRotation = Quaternion.LookRotation(aimPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);

            // weapon rotation independent of player rotation 
            weaponObject.transform.rotation = Quaternion.LookRotation(aimPoint - weaponObject.transform.position);

            // Show where the gun is aiming with a cyan line
            Debug.DrawRay(weaponObject.transform.GetChild(0).transform.position, weaponObject.transform.GetChild(0).transform.forward * 100, Color.cyan, 0);

            // Show where we're aiming with the offset included with a pink line
            Debug.DrawLine(weaponObject.transform.GetChild(0).transform.position, aimPoint, Color.magenta, 0);

            // move the crooshair to the location the player is aiming at
            crosshair.transform.position = aimPoint;
            // set the crosshair to the normal of what hit location
            crosshair.transform.rotation = Quaternion.LookRotation(hit.normal);

            // set the location of the laser pointer
            laserLine.SetPosition(0, weaponObject.transform.GetChild(0).position);
            laserLine.SetPosition(1, hit.point);
        }
    }

    /// <summary>
    /// handle movement
    /// </summary>
    private void movement()
    {
        float finalMovSpeed = movSpeed;

        // Speed boost system, currently no stamina system, possibly add later, or not at all?
        if (Input.GetButton("Sprint"))
        {
            finalMovSpeed = movSpeed + sprintModifier;
        }

        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDir *= finalMovSpeed * Time.deltaTime;

        // if the player isnt grounded, apply gravity
        if (!controller.isGrounded)
            moveDir.y -= GRAVITY * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }

    /// <summary>
    /// call use on an object in front of the player
    /// </summary>
    private void use()
    {
        // cast a ray in front of the player, check if distance is less than or equal to useRange

        // if it is, call the interact function on the object hit by the raycast
    }
	
    /// <summary>
    /// if the player dies, set the object to false to prevent nullrefrences [TODO: add actual cleaup code on player being killed or allow for respawning]
    /// </summary>
    protected override void kill(){ gameObject.SetActive(false); }


    /// <summary>
    /// set the player variables with the selected class data
    /// </summary>
    private void initializePlayerClass()
    {
        movSpeed = playerClass.movementSpeed;
        sprintModifier = playerClass.sprintSpeedModifier;

        health.maxResource = playerClass.maximumHealth;
        health.curResource = health.maxResource;

        energy.maxResource = playerClass.maximumEnergy;
        energy.curResource = energy.maxResource;

        defense = playerClass.baseDefense;
    }

    private void EquipWeapon(int WeaponIndex = 0)
    {
        EquippedWeapon = WeaponIndex;
        weaponController.equipWeapon(Weapons[WeaponIndex]);
    }

    private void EquipNextWeapon()
    {
        int nextWeapon = EquippedWeapon + 1;
        if (nextWeapon >= Weapons.Count)
        {
            nextWeapon = 0;
        }
        EquipWeapon(nextWeapon);
    }
}
