using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 10;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float chargePercent)
    {
        float bulletSpeed = baseSpeed * chargePercent;
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
