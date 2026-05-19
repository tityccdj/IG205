using UnityEngine;
using static GameManager;

public class InventoryManager : MonoBehaviour
{
    public enum ItemType
    {
        Bulb,       // ลำดับที่ 0
        Glass, // ลำดับที่ 1
        Plastic,  // ลำดับที่ 2
        Bottle,   // ลำดับที่ 3
        Bag,  // ลำดับที่ 4
        Straw,      // ลำดับที่ 5
        Banana,     // ลำดับที่ 6
        Apple    // ลำดับที่ 7
    }
    public enum BinType
    {
        Organic,    // ขยะย่อยสลาย/ขยะเปียก
        Recycle,    // ขยะรีไซเคิล
        General,    // ขยะทั่วไป
        Hazardous   // ขยะอันตราย
    }
    public static InventoryManager Instance;

    [Header("Inventory")]
    // สร้างกล่องเก็บของ 8 ช่อง (เดี๋ยวเราจะเอาตัวเลขจาก enum มาเป็นตัวบอกช่อง)
    public int[] inventoryCounts = new int[8];

    // ฟังก์ชันสำหรับ "เก็บไอเทมเข้ากระเป๋า"
    public void AddItem(ItemType type, int amount)
    {
        // แปลงชื่อไอเทม (enum) เป็นตัวเลข index เพื่อเอาไปยัดใส่ช่องให้ถูกต้อง
        int index = (int)type;
        inventoryCounts[index] += amount;

        Debug.Log($"เก็บ {type} เพิ่ม {amount} ชิ้น! ตอนนี้มีทั้งหมด {inventoryCounts[index]} ชิ้น");
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
