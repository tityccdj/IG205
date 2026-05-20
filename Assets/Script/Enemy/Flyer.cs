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
    Vector2 init;
    float lifetime = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth = 100 + (GameManager.Instance.CurrentLevel * 2f);
        CurrentHealth = MaxHealth;
        drop = GetComponent<EnemyDrop>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        init = transform.position;
        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
