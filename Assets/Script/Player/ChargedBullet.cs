using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 10f;
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
    public void Shoot()
    {
        rb.AddForce(transform.right * bulletspeed, ForceMode2D.Impulse);
    }
}
