using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int MaxHealth = 3;
    private int CurrentHealth = 3;


    private float moveSpeed = 0.5f;
    private bool isFacingRight = true;
    private Transform player;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MaxHealth = 3+GameManager.Instance.CurrentLevel;
        CurrentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }
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
}
