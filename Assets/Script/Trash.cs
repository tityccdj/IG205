using UnityEngine;

public class Trash : MonoBehaviour
{
    public InventoryManager.ItemType myTrashType; // ประเภทของขยะที่ไอเทมนี้เป็น
    public float lifeTime = 10f; // เวลาที่ขยะจะอยู่ในโลกก่อนที่จะหายไปเอง
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
    }
    public void UpdateLife()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject); // ขยะหายไปเองเมื่อหมดอายุ
        }
        else if (lifeTime <= 3f)
        {
            // ตัวอย่าง: ทำให้ขยะเริ่มกะพริบเมื่อใกล้หมดอายุ
            float alpha = Mathf.PingPong(Time.time * 5f, 1f); // กะพริบด้วยความเร็ว 5 ครั้งต่อวินาที
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (InventoryManager.Instance != null)
            {
                // 2. ส่ง "ชนิดขยะ" และ "จำนวน" ไปให้ระบบ Inventory จัดการ
                InventoryManager.Instance.AddItem(myTrashType, 1);

                // เก็บสำเร็จ ทำลายขยะที่พื้นทิ้ง
                Destroy(gameObject);
            }
        }
    }
}
