using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 13;
    [SerializeField] private float basedamage;
    public GameObject hitParticle;
    public GameObject FullhitParticle;
    public float randomRotationRange = 360f;
    float damage;
    Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 500f;
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
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
    public void Shoot(float chargePercent)
    {
        if (GameManager.Instance != null)
        {
            basedamage = GameManager.Instance.Damage * 3.5f;
        }
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
                Enemy enemy = collision.GetComponent<Enemy>();
                Flyer flyer = collision.GetComponent<Flyer>();

                if (enemy != null)
                {
                    if (damage >= basedamage * 1.5f)
                    {
                        Instantiate(FullhitParticle, transform.position, Quaternion.identity);
                        enemy.Knockback(0.7f, 10f);
                    }
                    else
                    {
                        Instantiate(hitParticle, transform.position, Quaternion.identity);
                        enemy.Knockback(0.4f, 5f);
                    }
                    enemy.TakeDamage(damage); // เรียกใช้ผ่านตัวแปร enemy ได้เลย ไม่ต้อง GetComponent ใหม่
                }
                else if(flyer != null)
                {
                    if (damage >= basedamage * 1.5f)
                    {
                        Instantiate(FullhitParticle, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(hitParticle, transform.position, Quaternion.identity);
                    }
                    flyer.TakeDamage(damage);
                }
            Destroy(gameObject);
        }
        
    }
}
