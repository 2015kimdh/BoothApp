using UnityEngine;

public class LetterboxCamera : MonoBehaviour
{
    public float targetAspect = 1080f / 1980f; // 목표 비율 (예: 16:9)

    void Start()
    {
        ApplyLetterbox();
    }

    void ApplyLetterbox()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f) // 가로가 좁은 경우 (검은 띠 추가)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else // 세로가 좁은 경우 (좌우 검은 띠 추가)
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}