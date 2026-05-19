using UnityEngine;

public class DraggingTrash : MonoBehaviour
{
    public InventoryManager.ItemType itemType;

    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private Collider2D col; // 🌟 เพิ่มตัวแปรมารับ Collider
    private TrashBin currentBin;
    public GameObject RightEffect;
    public GameObject WrongEffect;
    void Start()
    {
        RightEffect = Resources.Load<GameObject>("VFX/HouseScene/Right");
        WrongEffect = Resources.Load<GameObject>("VFX/HouseScene/Wrong");
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>(); // 🌟 ดึงคอมโพเนนต์มาเก็บไว้
    }

    private void OnMouseDown()
    {
        isDragging = true;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // ✨ ทริคความเนี้ยบ: พอเอามือจับปุ๊บ แปลงร่างเป็น Trigger (ทะลุวัตถุอื่นได้)
        // เพื่อไม่ให้มันลากไปกวาดขยะชิ้นอื่นในตะกร้ากระเด็น
        if (col != null)
        {
            col.isTrigger = true;
        }

        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (currentBin != null)
        {
            if (currentBin.IsCorrectTrash(itemType))
            {
                GameManager.Instance.GoodMoney++;
                Instantiate(RightEffect, transform.position, Quaternion.identity);
                Debug.Log($"Goodmoney: {GameManager.Instance.GoodMoney}");
                // หักของออกจาก Inventory ตรงนี้ได้ด้วยถ้าต้องการ
                Destroy(gameObject);
                return;
            }
            else
            {
                GameManager.Instance.FailedMoney++;
                
                Instantiate(WrongEffect, transform.position, Quaternion.identity);
                Debug.Log($"Failedmoney: {GameManager.Instance.FailedMoney}");
                Destroy(gameObject);
            }
        }

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // ✨ ทริคความเนี้ยบ: ปล่อยมือปุ๊บ หรือทิ้งผิดถัง ให้คืนร่างกลับมาชนกันเองได้เหมือนเดิม
        if (col != null)
        {
            col.isTrigger = false;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        // 🚨 สิ่งที่ต้องเพิ่มสำหรับกล้อง Perspective:
        // เราต้องหาระยะห่าง (ความลึกแกน Z) ระหว่าง "ตัววัตถุนี้" กับ "กล้องหลัก"
        // เพื่อบอก Unity ว่าเราจะแปลงพิกัดเมาส์ลงไปที่ระนาบความลึกเท่าไหร่ในโลก 3D
        float distanceToCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        // ยัดค่าความลึกเข้าไปในแกน Z ของตำแหน่งเมาส์บนจอก่อนที่จะแปลงพิกัด
        mousePoint.z = distanceToCamera;

        // สั่งแปลงพิกัด (คราวนี้จะแม่นยำเป๊ะๆ แล้วครับ)
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePoint);

        return worldPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashBin bin = collision.GetComponent<TrashBin>();
        if (bin != null)
        {
            currentBin = bin;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TrashBin bin = collision.GetComponent<TrashBin>();
        if (bin != null && currentBin == bin)
        {
            currentBin = null;
        }
    }
}
