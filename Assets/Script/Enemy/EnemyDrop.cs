using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Drop Items")]
    public GameObject[] itemsToDrop; // Array เก็บของที่ดรอปได้
    public float dropOffset=1;
    public void DropItem()
    {
        if (itemsToDrop.Length > 0)
        {
            int randomIndex = Random.Range(0, itemsToDrop.Length);
            GameObject selectedItem = itemsToDrop[randomIndex];
            Vector3 dropPosition = transform.position + new Vector3(0, dropOffset, 0);
            Instantiate(selectedItem, dropPosition, Quaternion.identity);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
