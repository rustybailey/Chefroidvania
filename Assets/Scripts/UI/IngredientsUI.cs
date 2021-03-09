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

    // TODO: Create a common animator that pulses the scale
    // TODO: In a coroutine, find all child ingredients and trigger their anim with a delay between each one's start
    // TODO: After X seconds, stop the anim.
    // TODO: Should we give them a background glow or something to highlight that you found them all?
}
