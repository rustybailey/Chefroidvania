using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Player player;

    private Canvas canvas;
    private GameObject resumeButton;
    private float buttonThrottleTime = .2f;
    private float countDown;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton = transform.Find("Navigation/Resume Button").gameObject;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        countDown = 0;
        inputManager = player.InputManager;
    }

    // Update is called once per frame
    void Update()
    {
        // Add a buffer so you can't spam it
        if (countDown > 0)
        {
            countDown -= Time.unscaledDeltaTime;
            return;
        }

        bool pauseWasPressed = Keyboard.current.pKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame;
        if (pauseWasPressed)
        {
            countDown = buttonThrottleTime;
            if (canvas.enabled)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        canvas.enabled = true;
        inputManager.UI.Enable();
        inputManager.Player.Disable();
        EventSystem.current.firstSelectedGameObject = resumeButton;
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
        inputManager.UI.Disable();
        inputManager.Player.Enable();
    }
}
