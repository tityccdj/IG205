using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 25f;
    [SerializeField] private float bulletDamage;
    [SerializeField] public GameObject hitParticle;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            bulletDamage = GameManager.Instance.Damage;
        }
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
            Enemy enemy = collision.GetComponent<Enemy>();
            Flyer flyer = collision.GetComponent<Flyer>();
            if (enemy != null)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                enemy.TakeDamage(bulletDamage);
                enemy.Knockback(0.2f, 2f);
            }
            else if(flyer != null)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                flyer.TakeDamage(bulletDamage);
                Debug.Log("Hitfly");
            }
            Destroy(gameObject);
        }
        
    }
}
