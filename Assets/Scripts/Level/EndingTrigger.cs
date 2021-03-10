using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] GameObject endingOverlay;
    [SerializeField] GameObject firstButton;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Awake()
    {
        endingOverlay.SetActive(false);
    }

    private void OnEnable()
    {
        inputManager = new InputManager();
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
        endingOverlay.SetActive(true);

        yield return new WaitForEndOfFrame();

        InputManager inputManager = collision.GetComponent<Player>().InputManager;
        inputManager.UI.Enable();
        inputManager.Player.Disable();
        EventSystem.current.firstSelectedGameObject = firstButton;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
