using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 20f;
    [SerializeField] private float bulletDamage = 10f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }

    public void Shoot()
    {
        rb.AddForce(transform.right * bulletspeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        
    }
}
