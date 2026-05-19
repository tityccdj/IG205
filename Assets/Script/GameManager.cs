using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    private TextMeshProUGUI Spawnbutton_txt;
    [Header("Player")]
    public int MaxHealth = 3;
    public float moveSpeed = 5f;
    public int DropChance = 30; // เปอร์เซ็นต์โอกาสที่ศัตรูจะดรอปไอเทม
    public float Damage = 10f; 

    [Header("Info")]
    public int CurrentLevel = 1;
    public int EnemiesDefeated = 0;
    public int GoodMoney = 0;
    public int FailedMoney = 0;

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

    public void BuyWithGoodMoney(int cost)
    {
        if (GoodMoney >= cost)
        {
            GoodMoney -= cost;
            Debug.Log($"ซื้อไอเทมสำเร็จ! เงินที่เหลือ: {GoodMoney}");
        }
        else
        {
            Debug.Log("เงินไม่พอสำหรับการซื้อไอเทมนี้!");
        }
    }

    public void BuyWithFailedMoney(int cost)
    {
        if (FailedMoney >= cost)
        {
            FailedMoney -= cost;
            Debug.Log($"ซื้อไอเทมสำเร็จ! เงินที่เหลือ: {FailedMoney}");
        }
        else
        {
            Debug.Log("เงินไม่พอสำหรับการซื้อไอเทมนี้!");
        }
    }
}
