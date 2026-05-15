using NUnit.Framework;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Player player;
    PlayerCombat combat;
    [Header("Dev mode info")]
    [SerializeField] private TextMeshProUGUI Hptext;
    [SerializeField] private TextMeshProUGUI Mstext;
    [SerializeField] public TextMeshProUGUI WeaponText;

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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        combat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        Hptext = GameObject.Find("hptext").GetComponent<TextMeshProUGUI>();
        Mstext = GameObject.Find("mstext").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpText(player.GetCurrentHealth());
        UpdateMsText(player.GetCurrentMovespeed());
        UpdateWeapon();
    }

    public void UpdateHpText(int hp)
    {
        Hptext.text = "HP: " + hp.ToString();
    }
    public void UpdateMsText(float ms)
    {
        Mstext.text = "MS: " + ms.ToString("F2");
    }

    public void UpdateWeapon()
    {
        WeaponText.text = combat.IschargedGunActive() ? "Weapon: Charged Gun" : "Weapon: Machine Gun";
    }

}
