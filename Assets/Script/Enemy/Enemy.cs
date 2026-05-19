using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MaxHealth = 50;
    private float CurrentHealth = 50;
    private bool isAlive = true;
    Animator anim;
    EnemyDrop drop;

    [Header("Movement")]
    [SerializeField] bool canMove = true;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] BoxCollider2D BoxCollider2D;
    private bool isFacingRight = true;
    private Transform player;
    Rigidbody2D rb;

    [Header("Attack")]
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isCooldown;
    public GameObject attackBox;
    private EnemySight sight;

    [Header("Feedback")]
    public float knockbackForce = 5f;
    public bool isKnockedback = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        sight = GetComponentInChildren<EnemySight>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        attackBox.SetActive(false);
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        drop = GetComponent<EnemyDrop>();
        isAttacking = false;
        MaxHealth = 50 + (GameManager.Instance.CurrentLevel * 1.5f);
        CurrentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        MoveTowardsPlayer();
        UpdateAttack();
        UpdateAnimations();
    }

    #region Movement
    public void MoveTowardsPlayer()
    {
        if(isKnockedback) return;

        if (player != null && canMove)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            CheckFlip(direction.x);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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

    #region Attack

    public void FinishAttack()
    {
        canMove = true;
        isAttacking = false;
        attackBox.SetActive(false);
        StartCoroutine(AttackCooldownRoutine());
    }
    IEnumerator Attack()
    {
        canMove = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        attackBox.SetActive(true); 

    }

    IEnumerator AttackCooldownRoutine()
    {
        isCooldown = true; // ÅçÍ¤ËéÒÁâ¨ÁµÕ
        yield return new WaitForSeconds(1); // ÃÍàÇÅÒµÒÁ·ÕèµÑé§äÇé
        isCooldown = false; // »Å´ÅçÍ¤! â¨ÁµÕÃÍºµèÍä»ä´é
    }
    public void UpdateAttack()
    {
        canAttack = sight.IsPlayerInSight();
        if (canAttack && !isAttacking && !isCooldown)
        {
            StartCoroutine(Attack());
        }
    }
    #endregion

    #region GetHit

    public void Knockback(float duration, float force)
    {
        StartCoroutine(ApplyKnockback(duration, force));
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
    private void Die()
    {
        StartCoroutine(DeadMove(0.5f));
        BoxCollider2D.enabled = false;
        canMove = false;
        attackBox.SetActive(false);
        isAttacking = false;
        isAlive = false;
        anim.SetTrigger("Dead");
        DropItem();

        StartCoroutine(DeadCd());
    }

    private void DropItem()
    {
        if (drop != null)
        {
            drop.DropItem();
        }
    }
    IEnumerator DeadMove(float duration)
        {
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
        }
    IEnumerator DeadCd()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator Feedback()
    {

        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.enabled = true;
    }
    IEnumerator ApplyKnockback(float duration, float force)
    {
        isKnockedback = true;
        Vector2 knockbackDirection = isFacingRight ? Vector2.left : Vector2.right;
        rb.linearVelocity = knockbackDirection * force;
        yield return new WaitForSeconds(duration);
        isKnockedback = false;
    }

    #endregion


    #region Animations
    public void UpdateAnimations()
        {
            anim.SetFloat("moveSpeed", Mathf.Abs(rb.linearVelocity.x));
            anim.SetBool("isAttacking", isAttacking);
        }
    #endregion


}
