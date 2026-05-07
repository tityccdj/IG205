using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public Transform Gunpivot;
    private Vector2 pointerInput;
    private Camera mainCam;
    public Vector2 direction;

    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.Find("Player").GetComponent<Player>();
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
        float flipX = player.isFacingRight ? 1 : -1;
        // 4. สั่งให้ปืนหมุน

        Gunpivot.rotation = Quaternion.Euler(0, 0, angle);
        if (angle > 90 || angle < -90)
        {
            Gunpivot.localScale = new Vector3(flipX, -1, 1);
        }
        else
        {
            Gunpivot.localScale = new Vector3(flipX, 1, 1);
        }

        // 5. นำค่าที่คำนวณเสร็จแล้วไปปรับขนาดปืน แค่บรรทัดเดียวจบ!


    }
}
