using UnityEngine;

public class DeadPanel : MonoBehaviour
{
    CanvasGroup canvas;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0f;
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead)
        {
            canvas.alpha +=(Time.deltaTime/2);
        }
    }
}
