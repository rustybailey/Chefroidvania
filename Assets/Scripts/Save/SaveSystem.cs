using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerSaveData playerSaveData)
    {
        string jsonData = JsonUtility.ToJson(playerSaveData);

        File.WriteAllText(Application.persistentDataPath + "/player_data.txt", jsonData);
    }

    public static PlayerSaveData LoadPlayer()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/player_data.txt");

        if (jsonData != null)
        {
            PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(jsonData);

            return playerSaveData;
        }

        return null;
    }
}
