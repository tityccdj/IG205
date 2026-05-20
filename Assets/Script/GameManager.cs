using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    [Header("Player")]
    public int MaxHealth = 3;
    public float moveSpeed = 5f;
    public int DropChance = 50; // เปอร์เซ็นต์โอกาสที่ศัตรูจะดรอปไอเทม
    public float Damage = 10f; 

    [Header("Info")]
    public int CurrentLevel = 1;
    public int GoodMoney = 0;
    public int FailedMoney = 0;
    [Header("Upgrade Costs (Use Good Money Only)")]
    public int costUpgradeHealth = 3;
    public int costUpgradeDrop = 5;
    public int costUpgradeDamage = 5;

    [Header("Development")]
    public bool SpawnEnemies = true;
    private void Awake()
    {
        transform.SetParent(null); // ทำให้ GameManager ไม่เป็นลูกของวัตถุอื่น เพื่อป้องกันการถูกทำลายเมื่อโหลดซีนใหม่
        if (Instance == null|| Instance == this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool BuyWithGoodMoney(int cost)
    {
        if (GoodMoney >= cost)
        {
            GoodMoney -= cost;
            Debug.Log($"ซื้อไอเทมสำเร็จ! เงินที่เหลือ: {GoodMoney}");
            return true;
        }
        else
        {
            Debug.Log("เงินไม่พอสำหรับการซื้อไอเทมนี้!");
            return false;
        }
    }

    public void ExchangeFailedToGood()
    {
        if (FailedMoney >= 3)
        {
            FailedMoney -= 3;
            GoodMoney += 1;
            Debug.Log($"แลกเงินสำเร็จ! GoodMoney: {GoodMoney} | FailedMoney เหลือ: {FailedMoney}");
        }
        else
        {
            Debug.Log("FailedMoney ไม่พอแลก! (ต้องใช้ 2 ชิ้น)");
        }
    }

    public void PurchaseUpgradeHealth()
    {
        if (BuyWithGoodMoney(costUpgradeHealth))
        {
            MaxHealth += 1;
            if(MaxHealth >= 6)
            {
                MaxHealth = 6;
                costUpgradeHealth = 999;
            }
            costUpgradeHealth += 2;
            Debug.Log($"อัปเกรดเลือดเป็น {MaxHealth}");
        }
    }


    public void PurchaseUpgradeDropChance()
    {
        // ?? เปลี่ยนมาใช้ BuyWithGoodMoney แล้ว
        if (BuyWithGoodMoney(costUpgradeDrop))
        {
            DropChance += 10;
            if (DropChance > 100)
            {
                DropChance = 100;
                costUpgradeDrop = 999;
            }
            costUpgradeDrop += 2;
            
            Debug.Log($"อัปเกรดโอกาสดรอปเป็น {DropChance}%");
        }
    }

    public void PurchaseUpgradeDamage()
    {
        // ?? เปลี่ยนมาใช้ BuyWithGoodMoney แล้ว
        if (BuyWithGoodMoney(costUpgradeDamage))
        {
            Damage += 2f;
            costUpgradeDamage += 1;
            Debug.Log($"อัปเกรดดาเมจเป็น {Damage}");
        }
    }
}
