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
    [SerializeField] private float maxChargeTime = 1.2f;
    [SerializeField] private float currentCharge = 0f;

    [Header("Charge Bar Visuals")]
    public GameObject chargeBarContainer; // กล่องเก็บหลอดชาร์จ (เพื่อสั่งเปิด/ปิดให้หายไปตอนไม่ได้ชาร์จ)
    public SpriteRenderer chargeBarFill; // ลาก ChargeB
    public Transform chargeMaskTransform;                                     // arFill มาใส่ช่องนี้
    public float ChargePercent => currentCharge / maxChargeTime;
    public float debugChargePercent =0f;
    private float initialFillScaleY;
    [Header("Mask Positions")]
    // 🚨 เอาค่าพิกัดที่คุณจดไว้ในสเต็ปที่ 2 มาใส่ตรงนี้ในหน้า Inspector ครับ
    public Vector3 maskEmptyPos; // พิกัดตอนหลอดว่าง
    public Vector3 maskFullPos;  // พิกัดตอนหลอดเต็ม

    [Header("Misc")]
    public GameObject Weapon;
    [SerializeField] public bool isAttacking = false;
    Player player;


    // เก็บค่าขนาด X เริ่มต้นของหลอดพลังตอนเต็ม (ปกติคือ 1)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
    }
    void Start()
    {
        isChargedGunActive = false;
        isMachneGunActive = true;
        UpdateWeapon();
        player = GetComponent<Player>();
        if (chargeBarFill != null)
        {
            initialFillScaleY = chargeBarFill.size.y;
        }

        // ซ่อนหลอดชาร์จไว้ก่อนตอนเริ่มเกม
        if (chargeBarContainer != null)
        {
            chargeBarContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead) return;
        UpdateMachineGun();
        UpdateChargedGun();
        UpdateShowWeapon();
        debugChargePercent = ChargePercent;

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
        currentCharge = 0f;
            if (chargeBarContainer != null)
            {
                chargeBarContainer.SetActive(false);
        }
    }

    public void OnAttack(InputValue input)
    {
        isAttacking = input.isPressed;
        if (isMachneGunActive)
        {
            isFiringMachineGun = input.isPressed;
        }
        else if (isChargedGunActive)
        {
            isCharging = input.isPressed;
            if (isCharging)            {
                if (chargeBarContainer != null)
                {
                    chargeBarContainer.SetActive(true);
                }
            }
            
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
        GameObject chragedbullet = Instantiate(ChargedBulletPrefab, ChargedBulletSpawn.transform.position, ChargedBulletSpawn.transform.rotation);
        ChargedBullet bulletScript = chragedbullet.GetComponent<ChargedBullet>();

        if (bulletScript != null)
        {
            bulletScript.Shoot(ChargePercent);
            Debug.Log($"Fired Charged Gun with Charge Percent: {ChargePercent * 100f}%");
        }


    }

    public void UpdateChargedGun()
    {
        if (!isChargedGunActive)
            return;
        if (isCharging)
        {
            currentCharge += Time.deltaTime;
            if (currentCharge >= maxChargeTime)
            {
                FireChargedGun();
                currentCharge = 0f; // Reset charge for next shot
            }
            UpdateChargeBarVisual();
        }
        else
        {

            if (chargeBarContainer != null)
            {
                chargeBarContainer.SetActive(false);    
            }
            if (currentCharge > 0.4f) // เช็คว่ามีการชาร์จมาบ้างนิดหน่อยแล้วถึงยิง
            {
                FireChargedGun();
                
            }
            currentCharge = 0f; // จากนั้นค่อยรีเซ็ตการชาร์จเป็น 0
            UpdateChargeBarVisual();
        }
    }

    private void UpdateChargeBarVisual()
    {
        if (chargeBarFill == null||player.isDead) return;

        currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
        float chargePercent = currentCharge / maxChargeTime;

        chargeMaskTransform.localPosition = Vector3.Lerp(maskEmptyPos, maskFullPos, chargePercent);
    

}

    public bool IschargedGunActive()
    {
        return isChargedGunActive;
    }
    #endregion

    #region Misc
    public void UpdateShowWeapon()
        {
            if (Weapon == null) return;
            Weapon.SetActive(isAttacking);
        }
    #endregion

}
