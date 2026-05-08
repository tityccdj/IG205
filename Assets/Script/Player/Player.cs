
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int CurrentHealth = 3;
    [SerializeField] private float CurrentMovespeed = 5.0f;
    [SerializeField] private GameObject Devmodbox;



    [Header("Movement")]
    [SerializeField] private bool DevMode;
    [SerializeField] public bool isFacingRight { get; private set; }
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float jumpForce=5.0f;
    [SerializeField] private bool isGrounded;
    private Rigidbody2D rb;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        isFacingRight = true;
        if (GameManager.Instance != null)
        {
            CurrentHealth = GameManager.Instance.MaxHealth;
            CurrentMovespeed = GameManager.Instance.moveSpeed;
        }
        Devmodbox = GameObject.Find("DevModeBox");
        Devmodbox.SetActive(false);
        DevMode = false;
        rb = GetComponent<Rigidbody2D>();

        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            DevMode = !DevMode;
            Devmodbox.SetActive(DevMode);
            UpdateDevMode();
        }

    }

    private void FixedUpdate()
    {
        UpdateMove(moveInput);
    }


    #region Movement
    public void OnJump(InputValue input)
    {
        if (isGrounded)
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

    public void UpdateDevMode()
    {
        if (DevMode)
        {
            jumpForce = 10;
            CurrentMovespeed = 10; // temp
        }
        else
        {
            jumpForce = 5;
            CurrentMovespeed = 5;
        }
    }

    #endregion

    #region Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
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
