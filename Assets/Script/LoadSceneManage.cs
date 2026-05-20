using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("UI Settings")]
    public Slider progressBar;

    [Header("Settings")]
    public float minimumLoadTime = 2.0f; // เวลาหน่วงขั้นต่ำ

    void Start()
    {
        // ป้องกันบัคเผื่อเผลอเปิดฉากนี้ตรงๆ โดยไม่ได้ระบุเป้าหมาย
        if (string.IsNullOrEmpty(SceneLoader.TargetSceneToLoad))
        {
            SceneLoader.TargetSceneToLoad = "Gameplay"; // ตั้งค่าเริ่มต้นเผื่อเหนียว
        }

        // พอฉากโหลดถูกเปิดขึ้นมาปุ๊บ ให้เริ่มโหลดฉากเป้าหมายทันที
        StartCoroutine(LoadTargetSceneRoutine());
    }

    private IEnumerator LoadTargetSceneRoutine()
    {
        // เริ่มโหลดฉากเป้าหมายแบบเบื้องหลัง
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.TargetSceneToLoad);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // 🌟 1. หาความคืบหน้าของการโหลดข้อมูลจริง (ตันที่ 1.0)
            float loadProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // 🌟 2. หาความคืบหน้าของเวลา (สมมติผ่านไป 1 วิ / ตั้งไว้ 2 วิ = 0.5)
            float timeProgress = Mathf.Clamp01(elapsedTime / minimumLoadTime);

            // 🌟 3. บังคับให้แสดงผลค่าที่ "น้อยกว่า"
            // - ถ้าโหลดเสร็จไว (load=1.0) หลอดจะค่อยๆ วิ่งตามเวลา (time)
            // - ถ้าคอมช้า โหลดไม่เสร็จสักที (load=0.2) หลอดก็จะวิ่งไปรอที่ 0.2
            float displayProgress = Mathf.Min(loadProgress, timeProgress);

            // อัปเดต UI หลอด
            if (progressBar != null) progressBar.value = displayProgress;

            // ถ้ารอเวลาครบ และโหลดข้อมูลเสร็จแล้ว
            if (operation.progress >= 0.9f && elapsedTime >= minimumLoadTime)
            {
                operation.allowSceneActivation = true; // ตัดเข้าฉากเกมจริงๆ!
            }

            yield return null;
        }
    }
}
