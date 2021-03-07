using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsUI : MonoBehaviour
{
    public void ShowIngredient(string name)
    {
        StartCoroutine(FadeImageToWhite(transform.Find(name).GetComponent<Image>()));
    }

    private IEnumerator FadeImageToWhite(Image image)
    {
        float elapsedTime = 0f;
        float totalTime = 2f;
        Color currentColor = image.color;

        while (elapsedTime < totalTime)
        {
            image.color = Color.Lerp(currentColor, Color.white, (elapsedTime / totalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
