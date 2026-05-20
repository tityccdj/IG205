using System.Collections;
using UnityEngine;

public class FlyerSpawner : MonoBehaviour
{
    private float spawnTimer = 0.0f;
    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private GameObject flyerPrefab;
    [SerializeField] private float spawnYRange = 2.0f;

    [Header("Warning Settings")]
    public GameObject warningSign;
    [SerializeField] private float warningDuration = 2.0f; // ระยะเวลาเตือนก่อนศัตรูโผล่
    [SerializeField] private float blinkInterval = 0.25f;  // ความเร็วในการกะพริบ (ยิ่งน้อยยิ่งกะพริบไว)

    void Start()
    {
        if (warningSign != null)
        {
            warningSign.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= spawnInterval)
        {
            // 🚨 สั่งเริ่มทำงาน Coroutine ตัวเดียว จบครบทั้งกะพริบและเสกศัตรู
            StartCoroutine(SpawnFlyerRoutine());

            spawnTimer = 0.0f;
            spawnInterval = Random.Range(7.0f, 10.0f); // สุ่มเวลารอบถัดไป
        }
    }

    void SpawnFlyer()
    {
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            Random.Range(transform.position.y - spawnYRange, transform.position.y + spawnYRange),
            0
        );
        Instantiate(flyerPrefab, spawnPos, Quaternion.identity);
    }

    // 🌟 รวมร่างเป็น Coroutine เดียว เพื่อความปลอดภัยและคุมง่าย
    IEnumerator SpawnFlyerRoutine()
    {
        float elapsed = 0f;

        // 💡 Loop กะพริบป้ายเตือนตามระยะเวลาที่กำหนด (เช่น 2 วินาที)
        while (elapsed < warningDuration)
        {
            if (warningSign != null)
            {
                // สลับสถานะ เปิด/ปิด ป้ายเตือน (!setActive แปลว่า ถ้าเปิดอยู่ให้ปิด ถ้าปิดอยู่ให้เปิด)
                warningSign.SetActive(!warningSign.activeSelf);
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        // ก่อนเสกศัตรู มั่นใจว่าปิดป้ายเตือนชัวร์ๆ
        if (warningSign != null)
        {
            warningSign.SetActive(false);
        }

        // เสกศัตรูบินได้ออกมา!
        SpawnFlyer();
    }
}