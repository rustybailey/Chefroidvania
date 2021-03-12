
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextLevelWithTransition()
    {
        StartCoroutine(InternalLoadNextLevelWithTransition());
    }

    // TODO: Generalize this transition behavior so that it doesn't just load the next level
    private IEnumerator InternalLoadNextLevelWithTransition()
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
        }
        else
        {
            Debug.Log("Transition failed: No Animator set on Level Loader");
        }
        yield return new WaitForSeconds(transitionTime);
        LoadNextLevel();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}