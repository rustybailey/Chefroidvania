using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveIconUI : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        SaveSystem.OnSavePlayer += ShowSaveIcon;
    }

    private void OnDisable()
    {
        SaveSystem.OnSavePlayer -= ShowSaveIcon;
    }

    private void ShowSaveIcon()
    {
        StartCoroutine(InternalShowSaveIcon());
    }

    private IEnumerator InternalShowSaveIcon()
    {
        // TODO: Actually fade in/out instead of just showing/hiding it

        image.enabled = true;
        
        float startTime = Time.time;

        while (Time.time - startTime < 3f)
        {
            yield return null;
        }

        image.enabled = false;
    }
}
