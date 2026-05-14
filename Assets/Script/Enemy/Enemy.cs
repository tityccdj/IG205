using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MaxHealth = 50;
    private float CurrentHealth = 50;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0.5f;
    private bool isFacingRight = true;
    private Transform player;
    Rigidbody2D rb;

    [Header("Feedback")]
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Start()
    {
        MaxHealth = 50 + (GameManager.Instance.CurrentLevel * 1.5f);
        CurrentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    #region Movement
    public void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            CheckFlip(direction.x);
        }
    }

    public void CheckFlip(float directionX)
    {
        if(directionX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (directionX < 0 && isFacingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    #endregion
    public void TakeDamage(float damage)
    {
        Debug.Log($"Enemy took {damage} damage");
        StartCoroutine(Feedback());
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator Feedback()
    {
        //float timer = 0;
        //while (timer < 0.1f)
        //{
        //    timer += Time.deltaTime;
        //    spriteRenderer.color = Color.red;
        //    yield return null;
        //}
        //spriteRenderer.color = originalColor;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
