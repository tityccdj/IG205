using UnityEngine;

public class WalkerSpawner : MonoBehaviour
{
    public int level;
    [SerializeField] private GameObject WalkerPrefab;
    [SerializeField] private float SpawnInterval; // ไม่ต้องใส่ 5.0f ตรงนี้แล้ว
    [SerializeField] private float maxRndRange = 1.0f;
    [SerializeField] private float minRndRange = 1.0f;
    private float SpawnTimer = 0.0f;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            level = GameManager.Instance.CurrentLevel;
        }
    }

    void Start()
    {
        // 💡 1. คำนวณขีดจำกัดเวลา โดยใช้ Mathf.Max 
        // ความหมายคือ: เอาค่าสมการมาเทียบกับ 3 ถ้าสมการน้อยกว่า 3 ให้ใช้เลข 3
        maxRndRange = Mathf.Max(3f, 7.5f - (level * 0.5f));
        minRndRange = Mathf.Max(1f, 4.3f - (level * 0.3f));

        // 💡 2. สุ่มเวลารอ "รอบแรก" ให้ตรงกับเลเวลปัจจุบันทันที
        SpawnInterval = Random.Range(minRndRange, maxRndRange);

        // เกิดตัวแรกทันที
        SpawnWalker();
    }

    // 💡 3. เปลี่ยนมาใช้ Update ปกติ (ระบบนับเวลาควรอยู่ใน Update ไม่ใช่ FixedUpdate ครับ)
    private void Update()
    {
        SpawnTimer += Time.deltaTime; // เปลี่ยนเป็น Time.deltaTime
        if (SpawnTimer >= SpawnInterval)
        {
            SpawnWalker();
            SpawnTimer = 0.0f;

            // สุ่มเวลารอรอบถัดไป
            SpawnInterval = Random.Range(minRndRange, maxRndRange);
        }
    }

    private void SpawnWalker()
    {
        if (WalkerPrefab != null)
        {
            Instantiate(WalkerPrefab, transform.position, Quaternion.identity);
        }
    }
}