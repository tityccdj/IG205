using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    [Header("MachineGun")]
    public GameObject MachineGun;   
    public GameObject MachineBulletPrefab;
    public GameObject MachineBulletSpawn;
    [SerializeField] private bool isMachneGunActive = true;
    [SerializeField] private float machineGunFireRate = 0.3f;
    [SerializeField] bool isFiringMachineGun = false;
    [SerializeField] private float nextFiretime = 0.3f;

    [Header("ChargedGun")]
    public GameObject ChargedGun;
    [SerializeField] private bool isChargedGunActive = false;
    [SerializeField] private GameObject ChargedBulletPrefab;
    [SerializeField] private GameObject ChargedBulletSpawn;
    [SerializeField] private bool isCharging = false;
    [SerializeField] private float chargedGunChargeTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
    }
    void Start()
    {
        isChargedGunActive = false;
        isMachneGunActive = true;
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMachineGun();
        UpdateChargedGun();
    }
    public void UpdateWeapon()
    {
        MachineGun.SetActive(isMachneGunActive);
        ChargedGun.SetActive(isChargedGunActive);
    }
    public void OnSwitchWeapon(InputValue input)
    {
        isMachneGunActive = !isMachneGunActive;
        isChargedGunActive = !isChargedGunActive;
        UpdateWeapon();
        isFiringMachineGun = false;
        isCharging = false;
    }

    public void OnAttack(InputValue input)
    {

        if (isMachneGunActive)
        {
            isFiringMachineGun = input.isPressed;
        }
        else if (isChargedGunActive)
        {
            isCharging = input.isPressed;
        }
    }
    #region MachineGun
    public void UpdateMachineGun()
    {
        if (!isMachneGunActive)
            return;
        nextFiretime -= Time.deltaTime;
        if (isFiringMachineGun && nextFiretime <= 0f)
        {
            FireMachineGun();
            nextFiretime = machineGunFireRate;
        }
    }
    
    public void FireMachineGun()
    {
        if (MachineBulletPrefab == null || MachineBulletSpawn == null)
        {
            return;
        }
        Instantiate(MachineBulletPrefab, MachineBulletSpawn.transform.position, MachineBulletSpawn.transform.rotation);
    }
    #endregion

    #region ChargedGun
    public void FireChargedGun()
    {
        if (ChargedBulletPrefab == null || ChargedBulletSpawn == null)
        {
            return;
        }
        if (isCharging)
        {
            Instantiate(ChargedBulletPrefab, ChargedBulletSpawn.transform.position, ChargedBulletSpawn.transform.rotation);
        }
    }

    public void UpdateChargedGun()
    {
        if (!isChargedGunActive)
            return;
        if (isCharging)
        {
            chargedGunChargeTime -= Time.deltaTime;
            if (chargedGunChargeTime <= 0f)
            {
                FireChargedGun();
                chargedGunChargeTime = 1.0f; // Reset charge time for next shot
            }
        }
    }
    #endregion
}
