using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 10;
    [SerializeField] private float basedamage = 40f;
    public GameObject hitParticle;
    public GameObject FullhitParticle;
    public float randomRotationRange = 360f;
    float damage;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        float randomRotation = Random.Range(-randomRotationRange, randomRotationRange);
        rb.rotation += randomRotation;
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
        if(chargePercent >= 0.95f)
        {
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (damage >= basedamage * 1.5f)
                {
                    Instantiate(FullhitParticle, transform.position, Quaternion.identity);
                    collision.GetComponent<Enemy>().Knockback(0.7f,10f);

                }
                else
                {
                    Instantiate(hitParticle, transform.position, Quaternion.identity);
                    collision.GetComponent<Enemy>().Knockback(0.4f,5f);
                }
                collision.GetComponent<Enemy>().TakeDamage(damage);
                
            }
            Destroy(gameObject);
        }
        
    }
}
