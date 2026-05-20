using UnityEngine;

public class WalkerSpawner : MonoBehaviour
{
     [SerializeField] private GameObject WalkerPrefab;
     [SerializeField] private float SpawnInterval = 5.0f;
   
    private float SpawnTimer = 0.0f;
     private void FixedUpdate()
     {
        SpawnTimer += Time.fixedDeltaTime;
         if (SpawnTimer >= SpawnInterval)
         {
             SpawnWalker();
             SpawnTimer = 0.0f;
             SpawnInterval = Random.Range(4.0f, 7.0f);
         }
     }
     private void SpawnWalker()
     {
         Instantiate(WalkerPrefab, transform.position, Quaternion.identity);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnWalker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
