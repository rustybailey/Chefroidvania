using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Navigation : MonoBehaviour
{
    const string START_BUTTON = "Start Button";
    const string CONTINUE_BUTTON = "Continue Button";
    const string RESTART_BUTTON = "Restart Button";
    const string MAIN_MENU_BUTTON = "Main Menu Button";
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
        //menuCursorAnimator = menuCursor.GetComponent<Animator>();
        audioManager = AudioManager.instance;
        eventSystem = EventSystem.current;
        levelLoader = FindObjectOfType<LevelLoader>();

        if (gameObject.name == QUIT_BUTTON && Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        }

        // TODO: If there is no save file, change the first selected object to New Game
        // and then gray out and remove Continue from the navigation hierarchy (just "disable" it?)
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
            audioManager.PlaySoundEffect("SpeechBubble02");
        }
    }

    public void ContinueGame()
    {
        Debug.Log("CONTINUE SUBMIT");
    }

    public void StartNewGame()
    {
        Debug.Log("START SUBMIT");
    }

    public void QuitGame()
    {
        levelLoader.QuitGame();
    }

    //public void OnSubmit(BaseEventData eventData)
    //{
    //    StartCoroutine(HandleSubmit());
    //}

    //public IEnumerator HandleSubmit()
    //{
    //    menuCursorAnimator.SetTrigger("Explode");
    //    audioManager.PlaySoundEffect("Menu Select");
    //    yield return new WaitForSeconds(.3f);
    //    menuCursor.GetComponent<SpriteRenderer>().enabled = false;
    //    yield return new WaitForSeconds(.3f);

    //    switch (gameObject.name)
    //    {
    //        case START_BUTTON:
    //        case CONTINUE_BUTTON:
    //            GameScore.instance.Reset();
    //            levelLoader.LoadNextLevelWithTransition();
    //            break;
    //        case RESTART_BUTTON:
    //            GameScore.instance.Reset();
    //            levelLoader.LoadPreviousScene();
    //            break;
    //        case MAIN_MENU_BUTTON:
    //            GameScore.instance.Reset();
    //            levelLoader.LoadMainMenu();
    //            break;
    //        case QUIT_BUTTON:
    //            levelLoader.QuitGame();
    //            break;
    //        default:
    //            Debug.Log("Can't submit: Unknown button name");
    //            break;
    //    }
    //}
}