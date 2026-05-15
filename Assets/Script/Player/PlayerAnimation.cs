using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] bool isAttacking;
     Animator anim;
     Player player;
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
        anim.SetBool("isAttacking", isAttacking);


    }
}
