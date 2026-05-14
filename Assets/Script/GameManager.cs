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

    [Header("Info")]
    public int CurrentLevel = 1;
    public int EnemiesDefeated = 0;

    [Header("Development")]
    public bool SpawnEnemies = true;
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
        Spawnbutton_txt = GameObject.Find("SpawnToggleButton").GetComponentInChildren<TextMeshProUGUI>();
        Spawnbutton_txt.text = SpawnEnemies ? "Stop Spawning" : "Start Spawning";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnToggle()
    {
        SpawnEnemies = !SpawnEnemies;
        Spawnbutton_txt.text = SpawnEnemies ? "Stop Spawning" : "Start Spawning";
    }
}
