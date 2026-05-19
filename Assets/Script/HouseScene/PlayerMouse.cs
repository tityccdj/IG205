using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 baseScale;

    void Start()
    {
        // จำขนาดดั้งเดิมตอนเริ่มเกม
        baseScale = transform.localScale;
    }

    void Update()
    {
        // 1. แปลงพิกัดเมาส์จากหน้าจอ (Screen) ให้เป็นพิกัดในโลกของเกม (World)
        Vector3 objectScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        // 2. ดึงตำแหน่งเมาส์บนหน้าจอ (อันนี้มันเป็น Screen Point อยู่แล้ว)
        Vector3 mouseScreenPos = Input.mousePosition;

        float absoluteScaleX = Mathf.Abs(baseScale.x);

        // 3. เทียบค่าแกน X บนหน้าจอกันตรงๆ เลย!
        if (mouseScreenPos.x < objectScreenPos.x)
        {
            // เมาส์อยู่ "ซ้ายมือ" ของ Object บนหน้าจอ
            transform.localScale = new Vector3(-absoluteScaleX, baseScale.y, baseScale.z);
        }
        else if (mouseScreenPos.x > objectScreenPos.x)
        {
            // เมาส์อยู่ "ขวามือ" ของ Object บนหน้าจอ
            transform.localScale = new Vector3(absoluteScaleX, baseScale.y, baseScale.z);
        }
    }
}
