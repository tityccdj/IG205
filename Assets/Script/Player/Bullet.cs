using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletspeed = 10f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate()
    {
        
    }

    public void Shoot()
    {
        rb.AddForce(transform.right * bulletspeed, ForceMode2D.Impulse);
    }
}
