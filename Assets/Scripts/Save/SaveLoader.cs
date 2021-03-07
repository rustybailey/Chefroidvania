using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader
{
    #region Component Variables
    Player player;
    #endregion

    #region Save Data Variables
    private string saveLocationName;
    #endregion

    public SaveLoader(Player player)
    {
        this.player = player;
    }

    public void LoadFromPlayerSaveData(PlayerSaveData saveData)
    {
        if (player == null)
        {
            return;
        }

        saveLocationName = saveData.saveLocationName;

        // If the current active scene is not the same as the saved scene, load
        // the saved scene and use the world position the was saved to move the
        // player to.
        if (SceneManager.GetActiveScene().name != saveData.sceneName)
        {
            if (SceneManager.GetSceneByName(saveData.sceneName) == null)
            {
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(saveData.sceneName);

            return;
        }

        FindSaveLocationAndMovePlayerToIt();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        player = Object.FindObjectOfType<Player>();

        FindSaveLocationAndMovePlayerToIt();
    }

    private void FindSaveLocationAndMovePlayerToIt()
    {
        SaveLocation[] saveLocations = SaveLocation.FindObjectsOfType<SaveLocation>();

        foreach (SaveLocation saveLocation in saveLocations)
        {
            if (saveLocation.GetSaveLocationName() == saveLocationName)
            {
                saveLocation.MovePlayerTo(player);

                break;
            }
        }
    }
}
