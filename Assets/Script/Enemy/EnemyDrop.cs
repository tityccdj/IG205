using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Drop Items")]
    public GameObject[] itemsToDrop; // Array เก็บของที่ดรอปได้
    public float dropOffset=1;
    public void DropItem()
    {
        int currentDropChance = 0;
        if (GameManager.Instance != null)
        {
            currentDropChance = GameManager.Instance.DropChance;
        }
        int roll = Random.Range(1, 101);

        // 🚨 3. เช็คว่าดวงดีพอที่จะดรอปไหม (ทอยได้น้อยกว่าหรือเท่ากับโอกาสที่มี)
        if (roll <= currentDropChance)
        {
            // ทำงานตามโค้ดเดิมของคุณ (สุ่มเลือกของใน Array มาดรอป)
            if (itemsToDrop.Length > 0)
            {
                int randomIndex = Random.Range(0, itemsToDrop.Length);
                GameObject selectedItem = itemsToDrop[randomIndex];
                Vector3 dropPosition = transform.position + new Vector3(0, dropOffset, 0);

                Instantiate(selectedItem, dropPosition, Quaternion.identity);
            }
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
