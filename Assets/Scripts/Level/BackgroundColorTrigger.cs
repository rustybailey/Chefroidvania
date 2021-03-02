using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorTrigger : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Color targetColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(FadeToDesiredColor());
        }
    }

    private IEnumerator FadeToDesiredColor()
    {
        float elapsedTime = 0f;
        float totalTime = 2f;
        Color currentColor = backgroundImage.color;

        while (elapsedTime < totalTime)
        {
            backgroundImage.color = Color.Lerp(currentColor, targetColor, (elapsedTime / totalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
