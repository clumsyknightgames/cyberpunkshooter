using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private float sensitivity;
    private Vector3 aimPoint;

    // scriptable object skills common methods: cast, canCast (checks if on cooldown, checks if enough energy)

    // Weapon variables
    private GameObject baseWeapon;
    private GameObject wepBarrel;
    public WeaponTemplate equippedWeapon;

    private float rateOfFire;
    private float fireTimer;
    private float weaponRange;

    private DamageRange wepDamageRange;

    private Mesh wepModel;
    private Material wepMaterial;

    public PlayerTemplate playerClass;

    void Start()
    {
        initializePlayerClass();

        baseWeapon = transform.GetChild(0).gameObject;
        wepBarrel = baseWeapon.transform.GetChild(0).gameObject;
        equipWeapon();

        sensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 8f);
        fireTimer = 0;
    }

    void Update()
    {
        movement();

        if(Input.GetMouseButton(0))
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= rateOfFire)
            {
                fireWeapon();
                fireTimer = 0;
            }
        }
    }
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            aimPoint = hit.point;
            Quaternion targetRotation = Quaternion.LookRotation(aimPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);
        }
    }
    private void movement()
    {
        // Speed boost system, currently no stamina system, possibly add later, or not at all?
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movSpeed = movSpeed + sprintModifier;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movSpeed = movSpeed - sprintModifier;
        }

        float hSpeed = (Input.GetAxis("Horizontal") * movSpeed) * Time.deltaTime;
        float vSpeed = (Input.GetAxis("Vertical") * movSpeed) * Time.deltaTime;

        Vector3 newPos = transform.position;
        newPos = new Vector3(newPos.x + hSpeed, newPos.y, newPos.z + vSpeed);

        transform.position = newPos;
    }

    private void initializePlayerClass()
    {
        setMovSpeed(playerClass.movementSpeed);
        setSprintModifier(playerClass.sprintSpeedModifier);

        setMaxHealth(playerClass.maximumHealth);
        setCurHealth(getMaxHealth());

        setMaxEnergy(playerClass.maximumEnergy);
        setCurEnergy(getMaxEnergy());

        setDefense(playerClass.baseDefense);
    }

    private void equipWeapon()
    {
        rateOfFire = equippedWeapon.WeaponRateOfFire;
        wepDamageRange = equippedWeapon.WeaponDamageRange;
        weaponRange = equippedWeapon.weaponRange;

        wepModel = equippedWeapon.WeaponModel;
        wepMaterial = equippedWeapon.weaponMaterial;


        baseWeapon.GetComponent<MeshFilter>().mesh = wepModel;
        baseWeapon.GetComponent<Renderer>().material = wepMaterial;
    }
    private void swapWeapon(WeaponTemplate newWeapon)
    {
        equippedWeapon = newWeapon;
        equipWeapon();
    }
    private void fireWeapon()
    {
        Vector3 targetPos = aimPoint;
        Debug.DrawLine(wepBarrel.transform.position, targetPos, Color.red, 10f);
    }
}
