using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private float sensitivity;
    private Vector3 aimPoint;

    private const float useRange = 3f;

    // scriptable object skills common methods: cast, canCast (checks if on cooldown, checks if enough energy)

    // Weapon variables
    public WeaponTemplate weaponTemplate;
    private GameObject baseWeapon;
    private Weapon weapon;

    public PlayerTemplate playerClass;

    void Start()
    {
        initializePlayerClass();

        baseWeapon = transform.GetChild(0).gameObject;
        weapon = baseWeapon.GetComponent<Weapon>();
        swapWeapon();

        sensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 8f);
    }

    void Update()
    {
        movement();

        if(Input.GetMouseButton(0))
        {
            weapon.fire(aimPoint);
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            use();
        }
    }
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            aimPoint.x = hit.point.x;
            aimPoint.z = hit.point.z;
            Quaternion targetRotation = Quaternion.LookRotation(aimPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("ground"))
            {
                aimPoint.y = hit.point.y;
            }
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

    private void use()
    {
        // cast a ray in front of the player, check if distance is less than or equal to useRange

        // if it is, call the interact function on the object hit by the raycast
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

    private void swapWeapon(WeaponTemplate newWeapon)
    {
        weaponTemplate = newWeapon;
        weapon.equipWeapon(newWeapon);
    }
    private void swapWeapon()
    {
        weapon.equipWeapon(weaponTemplate);
    }
}
