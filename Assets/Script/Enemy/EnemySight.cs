using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] private bool PlayerInSight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInSight = false;
        }
    }

    public bool IsPlayerInSight()
    {
        return PlayerInSight;
    }
}
