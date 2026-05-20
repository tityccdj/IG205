using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Prefabs & Sprites")]
    public GameObject maskPrefab;    // ลาก Prefab รูปหัวใจหรือหน้ากากมาใส่
    public Sprite fullMaskSprite;    // รูปตอนเลือดเต็ม (เอาไว้เผื่ออัปเดตรูป)

    // 🗑️ เอา emptyMaskSprite ออกไปแล้ว เพราะเราจะใช้วิธีซ่อนแทน

    private List<Image> spawnedMasks = new List<Image>();

    // 🚨 สั่งเรียกฟังก์ชันนี้ตอนเริ่มเกม เพื่อสร้างจำนวนไอคอนเลือดตาม HP สูงสุด
    public void SetupMaxHealth(int maxHealth)
    {
        // ล้างขยะเก่าออกก่อน
        foreach (Transform child in transform) Destroy(child.gameObject);
        spawnedMasks.Clear();

        // เสกไอคอนเลือดตามจำนวน Max HP
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newMask = Instantiate(maskPrefab, transform);
            Image maskImage = newMask.GetComponent<Image>();

            // ป้องกันบัคเผื่อลืมใส่รูปใน Inspector
            if (fullMaskSprite != null)
            {
                maskImage.sprite = fullMaskSprite;
            }

            spawnedMasks.Add(maskImage);
        }
    }

    // 🚨 สั่งเรียกฟังก์ชันนี้ทุกครั้งที่ผู้เล่น "โดนดาเมจ" หรือ "ฮีลเลือด"
    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < spawnedMasks.Count; i++)
        {
            // ถ้าลำดับของไอคอนเลือด น้อยกว่าหรือเท่ากับ เลือดที่เหลืออยู่
            if ((i+1) <= currentHealth)
            {
                // เลือดยังอยู่ -> เปิดการแสดงผล (โชว์รูป)
                spawnedMasks[i].gameObject.SetActive(true);
            }
            else
            {
                // เลือดหายไปแล้ว -> ปิดการแสดงผล (ซ่อนรูปทิ้งไปเลย)
                spawnedMasks[i].gameObject.SetActive(false);
            }
        }
    }
}