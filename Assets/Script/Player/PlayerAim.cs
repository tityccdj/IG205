using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public Transform Gunpivot;
    private Vector2 pointerInput;
    private Camera mainCam;
    public Vector2 direction;

    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimTracking();
    }
    public void OnAim(InputValue input)
    {
        pointerInput = input.Get<Vector2>();
    }
    private void AimTracking()
    {
        Vector2 mousepos = mainCam.ScreenToWorldPoint(pointerInput);
        direction = mousepos - (Vector2)Gunpivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. สั่งให้ปืนหมุน
        Gunpivot.rotation = Quaternion.Euler(0, 0, angle);

        // 5. พลิกปืนเมื่อเล็งไปข้างหลัง (ซ้าย)
        bool isAimingLeft = (angle > 90 || angle < -90);

        // 2. ตั้งค่าเริ่มต้นให้ปืนแกน Y เป็น 1 (ตั้งตรง)
        float flipY = 1f;

        // 3. ถ้าเมาส์อยู่ฝั่งซ้าย ให้พลิกปืนคว่ำลง (-1)
        if (isAimingLeft)
        {
            flipY = 1f;
        }

        // 4. จุดสำคัญ: เช็คว่าตัวละคร (Player) หันซ้ายอยู่หรือไม่?
        // ถ้าตัวละครหันซ้าย (แกน X ติดลบ) เราจะสลับค่า flipY อีกรอบ (ลบเจอลบเป็นบวก)
        if (player.localScale.x < 0)
        {
            flipY *= -1f;
        }

        // 5. นำค่าที่คำนวณเสร็จแล้วไปปรับขนาดปืน แค่บรรทัดเดียวจบ!
        Gunpivot.localScale = new Vector3(1, flipY, 1);

    }
}
