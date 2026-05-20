using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] bool isAttacking;
    bool isDead;
     Animator anim;
     Player player;
    [SerializeField] float moveSpeed;
    PlayerCombat combat;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
     private void Awake()
     {
         anim = GetComponent<Animator>();
         player = GameObject.FindWithTag("Player").GetComponent<Player>();
         combat = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        isAttacking = combat.isAttacking;
        moveSpeed = Mathf.Abs(player.GetFloatMove());
        anim.SetFloat("moveSpeed", moveSpeed);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isFalling", player.Isfalling());
        if (player.isDead)
        {
            anim.SetTrigger("isDead");
            anim.SetBool("isAttacking", false);
        }
    }
}
