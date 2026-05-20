using TMPro;
using UnityEngine;

public class GamePlayScene : MonoBehaviour
{
    public float time = 20f;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI leveltxt;
    public bool isPlayeralive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayeralive = true;
        leveltxt.text = "Level "+GameManager.Instance.CurrentLevel;
        time = 20 + (GameManager.Instance.CurrentLevel*10);
        if (time >= 90) time = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayeralive==false) return;
        timeText.text = Mathf.FloorToInt(time).ToString();
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            GoHome();
            GameManager.Instance.CurrentLevel ++;
            Debug.Log($"current level:{GameManager.Instance.CurrentLevel}");
        }
    }

    public void GoHome()
    {
        SceneLoader sceneloader = GetComponent<SceneLoader>();
        sceneloader.LoadScene("House");
    }
}
