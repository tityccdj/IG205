using System.Collections;
using TMPro;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
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
    [SerializeField] private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] private TextMeshProUGUI DropText;
    [SerializeField] private TextMeshProUGUI Leveltext;

    [Header("Animation Settings")]
    [SerializeField] private float animSpeed = 15f; // ความเร็วในการขยาย/หด
    private GameObject currentOpenPanel = null; // ตัวแปรจำว่าหน้าต่างไหนกำลังเปิดอยู่

    private void Start()
    {
        Leveltext.text = "Level: " + GameManager.Instance.CurrentLevel;

        // 1. จับทุกหน้าต่างย่อสเกลเป็น 0 ตั้งแต่เริ่มเกม
        SetInitialScaleZero(healthPanel);
        SetInitialScaleZero(dropChancePanel);
        SetInitialScaleZero(damagePanel);
        SetInitialScaleZero(ExChangePanel);

        // 2. ไม่มีหน้าต่างไหนเปิดอยู่
        currentOpenPanel = null;

        Updatemoney();
        UpdateStats();
    }

    private void Update()
    {
        // ให้ระบบคอยคำนวณการขยาย/หด ของหน้าต่างตลอดเวลาอย่างนุ่มนวล
        SmoothScalePanel(healthPanel);
        SmoothScalePanel(dropChancePanel);
        SmoothScalePanel(damagePanel);
        SmoothScalePanel(ExChangePanel);
    }

    // ฟังก์ชันย่อขนาดเป็น 0 ทันทีตอนเริ่มเกม
    private void SetInitialScaleZero(GameObject panel)
    {
        if (panel != null)
        {
            panel.transform.localScale = Vector3.zero;
            panel.SetActive(false);
        }
    }

    // ฟังก์ชันคำนวณอนิเมชั่น
    private void SmoothScalePanel(GameObject panel)
    {
        if (panel == null) return;

        // ถ้าแผงนี้คือแผงที่กำลังเปิด เป้าหมายสเกลคือ 1 (โชว์), ถ้าไม่ใช่เป้าหมายคือ 0 (ซ่อน)
        Vector3 targetScale = (panel == currentOpenPanel) ? Vector3.one : Vector3.zero;

        // ค่อยๆ ปรับขนาดปัจจุบันไปหาเป้าหมาย
        panel.transform.localScale = Vector3.Lerp(panel.transform.localScale, targetScale, Time.deltaTime * animSpeed);

        // จัดการเปิด/ปิด GameObject (SetActive) เพื่อประหยัดทรัพยากรตอนที่มองไม่เห็นแล้ว
        if (panel.transform.localScale.x > 0.05f && !panel.activeSelf)
        {
            panel.SetActive(true); // เปิดให้ทำงานเมื่อสเกลเริ่มขยาย
        }
        else if (panel.transform.localScale.x <= 0.05f && panel.activeSelf && targetScale == Vector3.zero)
        {
            panel.transform.localScale = Vector3.zero; // ล็อคให้เป็น 0 เป๊ะๆ กันเศษทศนิยม
            panel.SetActive(false); // ปิดการทำงานเมื่อสเกลหดจนมองไม่เห็นแล้ว
        }
    }

    // ==========================================
    // 🪟 ระบบเปิด/ปิดหน้าต่างแยกแต่ละอัน
    // ==========================================

    public void OpenHealthPanel()
    {
        currentOpenPanel = healthPanel; // บอกให้ระบบรู้ว่านี่คือหน้าต่างหลัก (เดี๋ยว Update จะขยายให้เอง)
        if (healthPriceTxt != null)
            healthPriceTxt.text = "x " + GameManager.Instance.costUpgradeHealth;
    }

    public void OpenDropChancePanel()
    {
        currentOpenPanel = dropChancePanel;
        if (dropChancePriceTxt != null)
            dropChancePriceTxt.text = "x " + GameManager.Instance.costUpgradeDrop;
    }

    public void OpenDamagePanel()
    {
        currentOpenPanel = damagePanel;
        if (damagePriceTxt != null)
            damagePriceTxt.text = "x " + GameManager.Instance.costUpgradeDamage;
    }

    public void OpenExchange()
    {
        currentOpenPanel = ExChangePanel;
        if (ExchangePriceTxt != null)
            ExchangePriceTxt.text = "x 3";
    }

    // ฟังก์ชันนี้ตอนนี้มีหน้าที่แค่ "ล้างค่า" เพื่อให้ทุกหน้าต่างหดกลับไปเป็น 0 ครับ
    public void CloseAllPanels()
    {
        currentOpenPanel = null;
    }

    // ==========================================
    // 🛒 ระบบปุ่มกดยืนยันการซื้อ
    // ==========================================

    public void BuyHealth()
    {
        GameManager.Instance.PurchaseUpgradeHealth();
        UpdateStats();
        Updatemoney();
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
        DropText.text = GameManager.Instance.DropChance + "%";
    }
}