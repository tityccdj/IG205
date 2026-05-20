using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    private float MaxHealth = 50;
    private float CurrentHealth = 50;
    [SerializeField] private float speed = 10f;
    EnemyDrop drop;
    SpriteRenderer spriteRenderer;
    Vector3 init;
    Vector3 originalScale;
    float lifetime = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
        MaxHealth = 80 + (GameManager.Instance.CurrentLevel * 20f);
        CurrentHealth = MaxHealth;
        drop = GetComponent<EnemyDrop>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float currentSpeed = 10f + (GameManager.Instance.CurrentLevel * 0.5f);

        // ล็อคความเร็วสูงสุดไว้ที่ไม่เกิน 25f (ความเร็วระดับพุ่งผ่านจอในเสี้ยววิ)
        speed = Mathf.Min(currentSpeed, 25f);

        init = transform.position;
        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        UpdateMovement();
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateMovement()
    {
        if (init.x > 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Feedback());
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    IEnumerator Feedback()
    {

        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.enabled = true;
    }
    protected void Die()
    {
        DropItem();

        Destroy(gameObject);
    }
    private void DropItem()
    {
        if (drop != null)
        {
            drop.DropItem();
        }
    }
}
