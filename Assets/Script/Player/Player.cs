
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int CurrentHealth = 3;
    [SerializeField] private float BaseMovespeed = 5.0f;
     private float CurrentMovespeed = 5.0f;
    public bool isDead;
    public GameObject deadbox;

    public HealthUI health_ui;


    [Header("Movement")]

    [SerializeField] public bool isFacingRight { get; private set; }
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float BasejumpForce = 5.0f;
    private float jumpForce=5.0f;
    [SerializeField] private bool isGrounded;
    private Rigidbody2D rb;
    private PolygonCollider2D playerCollider;
    private bool isFalling;
    float fall = 2.5f;
    [Header("Injure")]
    [SerializeField] private float invulnerabilityDuration = 1.0f;
    [SerializeField] private bool isInvulnerable;
    [SerializeField] SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        deadbox = GetComponentInChildren<Transform>().Find("DeadBox").gameObject;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerCollider = GetComponent<PolygonCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deadbox.SetActive(false);
        if (GameManager.Instance != null)
        {
            CurrentHealth = GameManager.Instance.MaxHealth;
            BaseMovespeed = GameManager.Instance.moveSpeed;
        }
        if (health_ui != null) health_ui.SetupMaxHealth(CurrentHealth);
        jumpForce = BasejumpForce;
        isFacingRight = true;
        CurrentMovespeed = BaseMovespeed;


        originalColor = spriteRenderer.color;

        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        if(isDead) return;
        UpdateMove(moveInput);
        if (rb.linearVelocity.y < 0)
        {
            isFalling = true;
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
    }
    

    #region Movement
    public void OnJump(InputValue input)
    {
        if (isGrounded&& !isDead)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void UpdateMove(Vector2 movement)
    {
        if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        rb.linearVelocity = new Vector2(movement.x * CurrentMovespeed, rb.linearVelocity.y);
    }
    public void Flip()
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }


    public float GetFloatMove()
    {
        return rb.linearVelocity.x;
    }
    public bool Isfalling()
    {
        return isFalling;
    }

    #endregion

    #region Collision&Trigger
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isFalling = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isInvulnerable)

            {
                TakeDamage(1);
                StartCoroutine(Invulerable(invulnerabilityDuration));
                TriggerHitStop(0.1f); // เรียกใช้ Hit Stop เมื่อโดนโจมตี
            }
        }
    }
   
    #endregion

    #region GetHit
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (health_ui != null) health_ui.UpdateHealthUI(CurrentHealth);
        
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    IEnumerator Invulerable(float duration)
    {
        isInvulnerable = true;
        StartCoroutine(FeedBackHit());
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    IEnumerator FeedBackHit()
    {         
        while (isInvulnerable)
        {
            spriteRenderer.color = Color.red; // เปลี่ยนสีเป็นแดง
            yield return new WaitForSeconds(0.1f); // รอ 0.1 วินาที
            spriteRenderer.color = originalColor; // คืนสีเดิม
            yield return new WaitForSeconds(0.1f); // รอ 0.1 วินาที
        }
        spriteRenderer.color = originalColor; // คืนสีเดิม
    }
    public void TriggerHitStop(float duration)
    {
        StartCoroutine(HitStopRoutine(duration));
    }

    IEnumerator HitStopRoutine(float duration)
    {
        Time.timeScale = 0f; // หยุดเวลาทั้งเกม (ตัวละครค้างกลางอากาศ)
        yield return new WaitForSecondsRealtime(duration); // ใช้ Realtime เพราะ timeScale เป็น 0 อยู่
        Time.timeScale = 1f; // คืนค่าเวลาให้เดินตามปกติ
    }


    public void Die()
    {
        rb.linearVelocity = Vector2.zero; 
        isDead = true;
        StartCoroutine(Restart());
        playerCollider.enabled = false; // ปิดการชนเพื่อไม่ให้เกิดปัญหาหลังจากตาย
        deadbox.SetActive(true);
        // เพิ่มการเล่นอนิเมชันตายที่นี่ (ถ้ามี)
    }

    IEnumerator Restart()
    {
        GamePlayScene gameplay = GameObject.FindAnyObjectByType<GamePlayScene>();
        gameplay.isPlayeralive = false;
        yield return new WaitForSeconds(5.0f);
        gameplay.GoHome();
    }

    #endregion

    #region Misc
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
    public float GetCurrentMovespeed()
    {
        return CurrentMovespeed;
    }
    #endregion
}
