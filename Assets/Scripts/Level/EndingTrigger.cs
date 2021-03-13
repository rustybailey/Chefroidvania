using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] GameObject endingOverlay;
    [SerializeField] GameObject firstButton;

    private Canvas endingOverlayCanvas;

    // Start is called before the first frame update
    void OnEnable()
    {
        // In win condition check's awake, the fridge is set to inactive,
        // so these things don't get a chance to get initialized on Awake,
        // so I changed it to onEnable
        endingOverlayCanvas = endingOverlay.GetComponent<Canvas>();
        //endingOverlayCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            StartCoroutine(HandleShowOverlay(collision));
        }
    }

    private IEnumerator HandleShowOverlay(Collider2D collision)
    {
        endingOverlayCanvas.enabled = true;

        yield return new WaitForEndOfFrame();

        InputManager inputManager = collision.GetComponent<Player>().InputManager;
        inputManager.UI.Enable();
        inputManager.Player.Disable();
        EventSystem.current.firstSelectedGameObject = firstButton;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
