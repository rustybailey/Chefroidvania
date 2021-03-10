using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    const string START_BUTTON = "Start Button";
    const string CONTINUE_BUTTON = "Continue Button";
    const string QUIT_BUTTON = "Quit Button";

    bool isInitialSelection = true;

    GameObject menuCursor;
    Animator menuCursorAnimator;
    AudioManager audioManager;
    EventSystem eventSystem;
    LevelLoader levelLoader;

    private void Start()
    {
        menuCursor = GameObject.Find("Menu Cursor");
        menuCursorAnimator = menuCursor.GetComponent<Animator>();
        audioManager = AudioManager.instance;
        eventSystem = EventSystem.current;
        levelLoader = FindObjectOfType<LevelLoader>();

        if (gameObject.name == QUIT_BUTTON && Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        }

        // If there is no save file, change the first selected object to New Game
        // and then gray out and disable Continue
        if (SaveSystem.LoadPlayer() == null)
        {
            eventSystem.firstSelectedGameObject = gameObject.transform.Find(START_BUTTON).gameObject;
            var continueButton = gameObject.transform.Find(CONTINUE_BUTTON).gameObject;
            continueButton.GetComponent<Button>().interactable = false;
            continueButton.GetComponentInChildren<TextMeshProUGUI>().alpha = 0.25f;
        }
    }

    public void MoveCursor(BaseEventData eventData)
    {
        menuCursor.transform.position = new Vector3(menuCursor.transform.position.x, eventData.selectedObject.transform.position.y);

        if (eventData.selectedObject.name == eventSystem.firstSelectedGameObject.name && isInitialSelection)
        {
            isInitialSelection = false;
        }
        else
        {
            audioManager.PlaySoundEffect("MenuUp");
        }
    }

    public void ContinueGame()
    {
        PlayerSaveData playerSaveData = SaveSystem.LoadPlayer();

        if (playerSaveData != null)
        {
            StartCoroutine(HandleContinueSubmit(playerSaveData));
        }
    }

    private IEnumerator HandleContinueSubmit(PlayerSaveData playerSaveData)
    {
        audioManager.PlaySoundEffect("MenuSelect");
        menuCursorAnimator.SetTrigger("destroy");
        yield return new WaitForSeconds(.8f);
        new SaveLoader(playerSaveData).LoadFromMainMenu();
    }

    public void StartNewGame()
    {
        audioManager.PlaySoundEffect("MenuSelect");
        menuCursorAnimator.SetTrigger("destroy");
        levelLoader.LoadNextLevelWithTransition();
    }

    public void QuitGame()
    {
        levelLoader.QuitGame();
    }

    public void GoToEnding()
    {
        audioManager.PlaySoundEffect("MenuSelect");
        menuCursorAnimator.SetTrigger("destroy");
        levelLoader.LoadNextLevelWithTransition();
    }

    public void GoToMainMenu()
    {
        audioManager.PlaySoundEffect("MenuSelect");
        menuCursorAnimator.SetTrigger("destroy");
        levelLoader.LoadMainMenu();
    }

    public void CloseEndingOverlay()
    {
        GameObject endingOverlay = GameObject.Find("Ending Overlay");
        endingOverlay.SetActive(false);

        GameObject player = GameObject.Find("Player");
        InputManager inputManager = player.GetComponent<Player>().InputManager;
        inputManager.UI.Disable();
        inputManager.Player.Enable();
    }
}