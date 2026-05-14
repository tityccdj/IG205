using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 10;
    [SerializeField] private float basedamage = 40f;
    float damage;
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
        damage = basedamage * chargePercent;
        damage = chargePercent>=0.95 ? damage*1.5f : damage;
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
