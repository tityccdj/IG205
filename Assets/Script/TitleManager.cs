using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameName;
    public GameObject ButtonMene;
    public GameObject TutorialPanel;

    [Header("Animation Settings")]
    [SerializeField] private float animSpeed = 15f;

    private bool isTutorialOpen = false;

    void Start()
    {
        // ย่อหน้า Tutorial เป็น 0 แล้วปิดไว้ก่อน
        if (TutorialPanel != null)
        {
            TutorialPanel.transform.localScale = Vector3.zero;
            TutorialPanel.SetActive(false);
        }

        // เปิดชื่อเกมและปุ่มให้ทำงานทันที
        if (gameName != null) gameName.SetActive(true);
        if (ButtonMene != null) ButtonMene.SetActive(true);

        isTutorialOpen = false;
    }

    void Update()
    {
        // ให้ระบบคอยคำนวณอนิเมชั่น "เฉพาะหน้า Tutorial" อันเดียวพอ
        Vector3 tutorialTarget = isTutorialOpen ? Vector3.one : Vector3.zero;
        SmoothScaleObject(TutorialPanel, tutorialTarget);
    }

    private void SmoothScaleObject(GameObject obj, Vector3 targetScale)
    {
        if (obj == null) return;

        obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, targetScale, Time.deltaTime * animSpeed);

        if (obj.transform.localScale.x > 0.05f && !obj.activeSelf)
        {
            obj.SetActive(true);
        }
        else if (obj.transform.localScale.x <= 0.05f && obj.activeSelf && targetScale == Vector3.zero)
        {
            obj.transform.localScale = Vector3.zero;
            obj.SetActive(false);
        }
    }

    // ==========================================
    // 🛒 ฟังก์ชันสำหรับปุ่มกด
    // ==========================================

    public void Opentutorial()
    {
        isTutorialOpen = true; // สั่งให้หน้าสอนเล่นค่อยๆ เด้งขยายขึ้นมา

        // ปิดชื่อเกมกับปุ่มแบบทันที
        gameName.SetActive(false);
        ButtonMene.SetActive(false);
    }

    public void CloseTutorial()
    {
        isTutorialOpen = false; // สั่งให้หน้าสอนเล่นค่อยๆ หดกลับไป

        // เปิดชื่อเกมกับปุ่มขึ้นมาทันที
        gameName.SetActive(true);
        ButtonMene.SetActive(true);
    }
}