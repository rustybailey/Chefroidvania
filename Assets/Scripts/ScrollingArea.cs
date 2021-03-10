using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingArea : MonoBehaviour
{
    [SerializeField] float startTime = 2f;
    [SerializeField] float scrollSpeed = 1f;

    private float currentScrollSpeed;
    private bool shouldStopScrolling = false;
    private GameObject navigation;

    // Start is called before the first frame update
    void Start()
    {
        currentScrollSpeed = scrollSpeed;
        navigation = GameObject.Find("Navigation");
        navigation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeforeScrollStart())
        {
            return;
        }

        ScrollText();
    }

    private bool IsBeforeScrollStart()
    {
        return Time.timeSinceLevelLoad <= startTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shouldStopScrolling = true;
        navigation.SetActive(true);
    }

    private void ScrollText()
    {
        if (!shouldStopScrolling)
        {
            transform.position += Vector3.up * currentScrollSpeed * Time.deltaTime;
        }
    }
}