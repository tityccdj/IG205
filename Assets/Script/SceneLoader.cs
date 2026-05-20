using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static string TargetSceneToLoad;

    // ไปผูกฟังก์ชันนี้กับปุ่ม (แล้วพิมพ์ชื่อฉากปลายทางลงในช่อง String ของปุ่ม)
    public void LoadScene(string sceneName)
    {
        // 1. จำชื่อฉากเป้าหมายเอาไว้
        TargetSceneToLoad = sceneName;

        // 2. โหลดไปที่หน้า Loading ทันที (ตั้งชื่อฉากโหลดให้ตรงด้วยนะครับ เช่น "LoadingScene")
        SceneManager.LoadScene("LoadingScene");
    }
}
