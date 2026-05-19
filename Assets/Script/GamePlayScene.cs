using TMPro;
using UnityEngine;

public class GamePlayScene : MonoBehaviour
{
    public float time = 20f;
    public TextMeshProUGUI timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = time.ToString("F2");
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            GoHome();
        }
    }

    public void GoHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("House");
    }
}
