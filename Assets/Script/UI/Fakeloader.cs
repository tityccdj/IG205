using UnityEngine;

public class Fakeloader : MonoBehaviour
{
    CanvasGroup canva;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canva = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        canva.alpha -=Time.deltaTime;
    }
}
