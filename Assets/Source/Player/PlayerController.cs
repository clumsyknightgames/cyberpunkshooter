using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private float sensitivity;
    private Vector3 aimPoint;

    private const float USE_RANGE = 3f;
    private const float AIM_OFFSET = 1f;
    private const float AIM_HEIGHT_DIFFERENCE_ALLOWANCE = 0.5f;

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

        if(Input.GetButton("Fire"))
        {
            weapon.fire();
        }
        
        if(Input.GetButton("Use"))
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
			aimPoint.y = hit.point.y;
			aimPoint.z = hit.point.z;

			// Yellow line for input-based aim before any corrections
			Debug.DrawLine(baseWeapon.transform.GetChild(0).transform.position, aimPoint, Color.yellow, 0);

			if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("ground")) && ((hit.point.y) > baseWeapon.transform.GetChild(0).transform.position.y)) aimPoint.y = hit.point.y + AIM_OFFSET * hit.normal.y * Mathf.Clamp(hit.point.y - baseWeapon.transform.GetChild(0).transform.position.y, 0, 1);
            
			/* old code, only for reference until aiming is working completely as intended.
			{
                if ((hit.point.y) > baseWeapon.transform.GetChild(0).transform.position.y) // are we aiming above where the player currently is
                    aimPoint.y = hit.point.y + AIM_OFFSET;
                else if ((hit.point.y) < baseWeapon.transform.GetChild(0).transform.position.y) // are we aiming below where the player currently is
                    aimPoint.y = hit.point.y - AIM_OFFSET;
                else
                    aimPoint.y = baseWeapon.transform.GetChild(0).transform.position.y;
			}*/

			// Rotate player towards new aim point
            Quaternion targetRotation = Quaternion.LookRotation(aimPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);

			// weapon rotation independent of player rotation 
			baseWeapon.transform.rotation = Quaternion.LookRotation(aimPoint - baseWeapon.transform.position);

			// Show where the gun is aiming with a cyan line
			Debug.DrawRay(baseWeapon.transform.GetChild(0).transform.position, baseWeapon.transform.GetChild(0).transform.forward * 100, Color.cyan, 0);

			// Show where we're aiming with the offset included with a pink line
			Debug.DrawLine(baseWeapon.transform.GetChild(0).transform.position, aimPoint, Color.magenta, 0);
        }
    }
    private void movement()
    {
        float finalMovSpeed = movSpeed;
        // Speed boost system, currently no stamina system, possibly add later, or not at all?
        if (Input.GetButton("Sprint"))
        {
            finalMovSpeed = movSpeed + sprintModifier;
        }

        float hSpeed = (Input.GetAxis("Horizontal") * finalMovSpeed) * Time.deltaTime;
        float vSpeed = (Input.GetAxis("Vertical") * finalMovSpeed) * Time.deltaTime;

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
