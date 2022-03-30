using System.Collections;
using UnityEngine;
using UnityEngine.UI;

static public class ExtensionMethods
{
    // Image Fading
    static public void ScreenFade(this Image spriteImage, float opacity, float duration)
    {
        if (spriteImage.IsActive()) spriteImage.StartCoroutine(CoruScreenFade(spriteImage, opacity, duration));
    }

    static public IEnumerator CoruScreenFade(Image spriteImage, float opacity, float duration)
    {
        float timeElapsed = 0;
        float startValue = spriteImage.color.a;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0, timeElapsed / duration);
            spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, newAlpha);
            yield return null;
        }
        spriteImage.StopAllCoroutines();
        spriteImage.gameObject.SetActive(false);
    }
}
