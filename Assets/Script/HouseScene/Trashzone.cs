using System.Collections;
using UnityEngine;

public class Trashzone : MonoBehaviour
{
    [Header("Basket Setup")]
    public Transform dropSpawnPoint; // ลาก SpawnPoint ที่ลอยอยู่เหนือตะกร้ามาใส่
    public float dropWidth = 2.0f;   // ความกว้างของปากตะกร้า (สุ่มให้ของตกลงมาไม่ซ้ำจุดเดิม)

    [Header("Item Prefabs")]
    [Tooltip("ใส่ Prefab ขยะที่มี Rigidbody2D ให้ตรงลำดับ (0=Bulb, 1=Glass, ...)")]
    public GameObject[] physicsItemPrefabs = new GameObject[8];

    void Start()
    {
        // (Option) คุณอาจจะอยากให้ของในตะกร้าหายไปตอนเริ่มเกมก็ได้
        PourItemsIntoBasket();
    }
    // เรียกฟังก์ชันนี้ตอนที่คุณเปิดหน้ากระเป๋าดู
    public void PourItemsIntoBasket()
    {
        // 1. (Option) ล้างของเก่าในตะกร้าทิ้งก่อน จะได้ไม่งอกซ้ำซ้อน
        ClearBasket();

        // 2. เริ่มเทของทีละชิ้น
        StartCoroutine(PourRoutine());
    }

    private IEnumerator PourRoutine()
    {
        if (InventoryManager.Instance == null) yield break;
        int[] currentItems = InventoryManager.Instance.inventoryCounts;

        // วนลูปตามชนิดขยะทั้ง 8 ชนิด
        for (int typeIndex = 0; typeIndex < currentItems.Length; typeIndex++)
        {
            int amount = currentItems[typeIndex];

            // วนลูปตามจำนวนของชิ้นนั้นๆ
            for (int i = 0; i < amount; i++)
            {
                // สุ่มตำแหน่งแกน X นิดหน่อย จะได้ไม่ตกลงมาซ้อนกันเป๊ะๆ
                float randomX = Random.Range(-dropWidth / 2f, dropWidth / 2f);
                Vector3 spawnPos = dropSpawnPoint.position + new Vector3(randomX, 0, 0);

                // สุ่มองศาการหมุนของไอเทม (จะได้กลิ้งๆ ดูเป็นธรรมชาติ)
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));

                // เสกไอเทมออกมาให้ร่วงลงไป!
                Instantiate(physicsItemPrefabs[typeIndex], spawnPos, randomRotation, transform);

                // หน่วงเวลาเล็กน้อย (0.1 วิ) ให้มันค่อยๆ ร่วงลงมาทีละชิ้น ไม่ใช่โผล่มาตู้มเดียว
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    private void Update()
    {
        // สำหรับทดสอบ: กดปุ่ม T เพื่อเทของลงตะกร้า
        if (Input.GetKeyDown(KeyCode.T))
        {
            PourItemsIntoBasket();
        }
    }
    // ฟังก์ชันสำหรับล้างตะกร้า
    public void ClearBasket()
    {
        // ทำลายลูกๆ ทุกตัวที่อยู่ในตะกร้าทิ้ง
        foreach (Transform child in transform)
        {
            // ข้ามไม่ให้มันลบกำแพง หรือ SpawnPoint 
            if (child.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
