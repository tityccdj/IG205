using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    private Vector2 pointerInput;
    private Camera mainCam;
    public Vector2 direction;

    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        AimTracking();
    }
    public void OnAim(InputValue input)
    {
        pointerInput = input.Get<Vector2>();
    }
    private void AimTracking()
    {
        Vector2 mousepos = mainCam.ScreenToWorldPoint(pointerInput);
        direction = mousepos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float flipX = player.isFacingRight ? 1 : -1;
        // 4. ÊÑè§ãËé»×¹ËÁØ¹

        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(flipX, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipX, 1, 1);
        }



    }
}
