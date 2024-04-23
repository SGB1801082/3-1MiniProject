using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutEffect : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2.0f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0.0f;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // 페이드 아웃 완료 후 비활성화
    }
}