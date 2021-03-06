using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerSaveData playerSaveData)
    {
        Debug.Log("Save player data");

        string jsonData = JsonUtility.ToJson(playerSaveData);

        File.WriteAllText(Application.persistentDataPath + "/player_data.txt", jsonData);
    }

    public static Player LoadPlayer()
    {
        Debug.Log("Load plyaer data");

        return null;
    }
}
