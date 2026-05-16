using UnityEngine;

public class DecoWalker : MonoBehaviour
{
    public float speed = 2f;
   
    Vector2 init;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        init = transform.position;
        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float speedMultiplier = Random.Range(0.8f, 1.5f);
        speed *= speedMultiplier;
        Destroy(gameObject, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (init.x > 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

}
