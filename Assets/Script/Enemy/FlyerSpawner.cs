using System.Collections;
using UnityEngine;

public class FlyerSpawner : MonoBehaviour
{
    public int level; // 🚨 เพิ่มตัวแปร level
    private float spawnTimer = 0.0f;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject flyerPrefab;
    [SerializeField] private float spawnYRange = 2.0f;
    [SerializeField] private float maxRndRange;
    [SerializeField] private float minRndRange;

    [Header("Warning Settings")]
    public GameObject warningSign;
    [SerializeField] private float warningDuration = 2.0f; // ระยะเวลาเตือนก่อนศัตรูโผล่
    [SerializeField] private float blinkInterval = 0.25f;  // ความเร็วในการกะพริบ

    private void Awake()
    {
        // 🚨 ดึงค่า Level มาจาก GameManager
        if (GameManager.Instance != null)
        {
            level = GameManager.Instance.CurrentLevel;
        }
    }

    void Start()
    {
        if (warningSign != null)
        {
            warningSign.SetActive(false);
        }

        // 💡 ตั้งค่าสมการให้ "ช้ากว่า" Walker
        // ตัวบินจะเริ่มที่ 12 วิ ลดเลเวลละ 0.5 (แต่ไม่ต่ำกว่า 6 วิ)
        maxRndRange = Mathf.Max(6.0f, 12.0f - (level * 0.5f));

        // ตัวบินเริ่มไวสุดที่ 7 วิ ลดเลเวลละ 0.3 (แต่ไม่ต่ำกว่า 4 วิ)
        minRndRange = Mathf.Max(4.0f, 7.0f - (level * 0.3f));

        // สุ่มเวลารอบแรก
        spawnInterval = Random.Range(minRndRange, maxRndRange);
    }

    // 💡 ย้ายมาใช้ Update และ Time.deltaTime แทน FixedUpdate
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            // สั่งเริ่มทำงาน Coroutine กะพริบป้ายเตือน
            StartCoroutine(SpawnFlyerRoutine());

            spawnTimer = 0.0f;
            // สุ่มเวลารอบถัดไปตามเลเวลปัจจุบัน
            spawnInterval = Random.Range(minRndRange, maxRndRange);
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

    IEnumerator SpawnFlyerRoutine()
    {
        float elapsed = 0f;

        while (elapsed < warningDuration)
        {
            if (warningSign != null)
            {
                warningSign.SetActive(!warningSign.activeSelf);
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (warningSign != null)
        {
            warningSign.SetActive(false);
        }

        SpawnFlyer();
    }
}