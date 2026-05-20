using TMPro;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Panels (หน้าต่างของแต่ละอัปเกรด)")]
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject dropChancePanel;
    [SerializeField] private GameObject damagePanel;
    [SerializeField] private GameObject ExChangePanel;

    [Header("Price Texts (ข้อความโชว์ราคา)")]
    [SerializeField] private TextMeshProUGUI healthPriceTxt;
    [SerializeField] private TextMeshProUGUI dropChancePriceTxt;
    [SerializeField] private TextMeshProUGUI damagePriceTxt;
    [SerializeField] private TextMeshProUGUI ExchangePriceTxt;

    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI GoodMoneyTxt;
    [SerializeField] private TextMeshProUGUI FailedMoneyTxt;

    [Header("Stats")]
    [SerializeField]private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] private TextMeshProUGUI DropText;
    [SerializeField] private TextMeshProUGUI Leveltext;
    private void Start()
    {
        Leveltext.text = "Level: "+GameManager.Instance.CurrentLevel;
        // ปิดทุกหน้าต่างตอนเริ่มเกมเพื่อความชัวร์
        CloseAllPanels();
        Updatemoney();
        UpdateStats();
    }

    // ==========================================
    // 🪟 ระบบเปิด/ปิดหน้าต่างแยกแต่ละอัน
    // ==========================================

    public void OpenHealthPanel()
    {
        CloseAllPanels(); // สั่งปิดหน้าต่างอื่นก่อนกันมันซ้อนทับกัน
        healthPanel.SetActive(true);

        // 🌟 ดึงราคาจาก GameManager มาโชว์ที่ Text ทันที
        if (healthPriceTxt != null)
            healthPriceTxt.text = "x " + GameManager.Instance.costUpgradeHealth;
    }

    public void OpenDropChancePanel()
    {
        CloseAllPanels();
        dropChancePanel.SetActive(true);

        if (dropChancePriceTxt != null)
            dropChancePriceTxt.text = "x " + GameManager.Instance.costUpgradeDrop;
    }

    public void OpenDamagePanel()
    {
        CloseAllPanels();
        damagePanel.SetActive(true);

        if (damagePriceTxt != null)
            damagePriceTxt.text = "x " + GameManager.Instance.costUpgradeDamage;
    }
    public void OpenExchange()
    {
        CloseAllPanels();
        ExChangePanel.SetActive(true);
        if (ExchangePriceTxt != null)
            ExchangePriceTxt.text = "x 3";
    }

    // ฟังก์ชันตัวช่วยสำหรับปิดทุกหน้าต่างพร้อมกัน
    public void CloseAllPanels()
    {
        if (healthPanel != null) healthPanel.SetActive(false);
        if (dropChancePanel != null) dropChancePanel.SetActive(false);
        if (damagePanel != null) damagePanel.SetActive(false);
        if (ExChangePanel != null) ExChangePanel.SetActive(false);
    }

    // ==========================================
    // 🛒 ระบบปุ่มกดยืนยันการซื้อ
    // ==========================================

    public void BuyHealth()
    {
        GameManager.Instance.PurchaseUpgradeHealth();
        UpdateStats();
        Updatemoney();
        // อัปเดตตัวเลขราคาใหม่หลังจากซื้อสำเร็จ
        if (healthPriceTxt != null) healthPriceTxt.text = "x " + GameManager.Instance.costUpgradeHealth;
    }

    public void BuyDropChance()
    {
        GameManager.Instance.PurchaseUpgradeDropChance();
        UpdateStats();
        Updatemoney();
        if (dropChancePriceTxt != null) dropChancePriceTxt.text = "x " + GameManager.Instance.costUpgradeDrop;
    }

    public void BuyDamage()
    {
        GameManager.Instance.PurchaseUpgradeDamage();
        UpdateStats();
        Updatemoney();
        if (damagePriceTxt != null) damagePriceTxt.text = "x " + GameManager.Instance.costUpgradeDamage;
    }
    public void Exchange()
    {
        GameManager.Instance.ExchangeFailedToGood();
        Updatemoney();
        if (ExchangePriceTxt != null) ExchangePriceTxt.text = "x 3";
    }

    public void Updatemoney()
    {
        GoodMoneyTxt.text = GameManager.Instance.GoodMoney.ToString();
        FailedMoneyTxt.text = GameManager.Instance.FailedMoney.ToString();
    }

    public void UpdateStats()
    {
        HealthText.text = GameManager.Instance.MaxHealth.ToString();
        DamageText.text = GameManager.Instance?.Damage.ToString();
        DropText.text = GameManager.Instance.DropChance+"%";
    }
}
