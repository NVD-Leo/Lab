using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Renderer objectRenderer; // Renderer của đối tượng
    public float fadeDuration = 5f; // Thời gian làm mờ (giây)

    private Material objectMaterial; // Vật liệu của đối tượng
    private bool isFading = false;   // Kiểm tra trạng thái làm mờ

    void Start()
    {
        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material;
        }
        else
        {
            Debug.LogError("Renderer chưa được gán!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        if (objectMaterial == null) yield break;

        isFading = true; // Đặt trạng thái đang làm mờ
        Color initialColor = objectMaterial.color; // Màu ban đầu
        float elapsedTime = 0f; // Thời gian đã trôi qua

        // Giảm dần giá trị alpha trong khoảng thời gian `fadeDuration`
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0f, elapsedTime / fadeDuration);
            objectMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null; // Đợi đến frame tiếp theo
        }

        // Đảm bảo alpha về 0 chính xác
        objectMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        isFading = false; // Kết thúc trạng thái làm mờ
    }
}
