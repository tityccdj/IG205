using UnityEngine;

public class WalkAniEvent : MonoBehaviour
{
    private Enemy enemyParent;
    private void Awake()
    {
        // สั่งให้ตัวลูก มองหาสคริปต์ Enemy ที่อยู่บนตัวแม่
        enemyParent = GetComponentInParent<Enemy>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FinshAttack()
    {         
        if (enemyParent != null)
        {
            enemyParent.FinishAttack();
        }
    }
}
