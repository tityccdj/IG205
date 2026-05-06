
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject Gun;
    public GameObject Sword;
    [SerializeField] private bool isGunActive = true;
    [SerializeField] private bool isSwordActive = false;

    [Header("Movement")]
    [SerializeField] private bool no_clip;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float moveSpeed=5.0f;
    [SerializeField] private float jumpForce=5.0f;
    [SerializeField] private bool isGrounded;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isSwordActive = false;
        isGunActive = true;
        isFacingRight = true;
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            no_clip = !no_clip;
            UpdateNoClip();
        }
       
    }

    private void FixedUpdate()
    {
        UpdateMove(moveInput);
    }

    #region Weapon
    public void UpdateWeapon()
    {
        Gun.SetActive(isGunActive);
        Sword.SetActive(isSwordActive);
    }
    public void OnSwitchWeapon(InputValue input)
    {
        isGunActive = !isGunActive;
        isSwordActive = !isSwordActive;
        UpdateWeapon();
    }
    #endregion

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

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
    public void Flip()
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    public void UpdateNoClip()
    {
        if (no_clip)
        {
            jumpForce = 10;
            moveSpeed = 10; // temp
        }
        else
        {
            jumpForce = 5;
            moveSpeed = 5;
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


}
