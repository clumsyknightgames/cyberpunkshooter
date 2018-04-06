using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float BASE_MOVEMENT_SPEED = 4f;
    private float movSpeed;
    private float curHealth, maxHealth;
    private float curEnergy, maxEnergy;
    private float defense;

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

    private Damage damageRange;

    private Mesh wepModel;
    private Material wepMaterial;

    void Start()
    {
        baseWeapon = transform.GetChild(0).gameObject;
        wepBarrel = baseWeapon.transform.GetChild(0).gameObject;

        movSpeed = BASE_MOVEMENT_SPEED;
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

    private void equipWeapon()
    {
        rateOfFire = equippedWeapon.WeaponRateOfFire;
        damageRange = equippedWeapon.WeaponDamageRange;
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

    private void movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movSpeed = BASE_MOVEMENT_SPEED*2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movSpeed = BASE_MOVEMENT_SPEED;
        }
        // input horizontal and vertical axis movment and translate location
        float hSpeed = (Input.GetAxis("Horizontal") * movSpeed) * Time.deltaTime;
        float vSpeed = (Input.GetAxis("Vertical") * movSpeed) * Time.deltaTime;

        Vector3 newPos = transform.position;
        newPos = new Vector3(newPos.x + hSpeed, newPos.y, newPos.z + vSpeed);

        transform.position = newPos;
    }

    public float getCurEnergy()
    {
        return curEnergy;
    }
    public void modifyCurEnergy(int amount)
    {
        curEnergy += amount;
        if (curEnergy > maxEnergy)
            curEnergy = maxEnergy;
    }

    public bool damage(int amount)
    {
        bool isDead;

        float dmg = amount - defense;
        curHealth -= dmg;

        isDead = curHealth <= 0;
        return isDead;
    }

    public bool heal(int amount)
    {
        if (curHealth < maxHealth)
        {
            curHealth += amount;
            if (curHealth > maxHealth)
                curHealth = maxHealth;
            return true; // was able to heal player
        }
        else
            return false; // was unable to healer player
    }


}
