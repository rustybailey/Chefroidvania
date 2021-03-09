using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const string SAVE_FILE = "player_save.txt";

    public delegate void BroadcastSave();
    public static event BroadcastSave OnSavePlayer;
    public static void SavePlayer(PlayerSaveData playerSaveData)
    {
        OnSavePlayer?.Invoke();
        string jsonData = JsonUtility.ToJson(playerSaveData);

        File.WriteAllText(Application.persistentDataPath + "/" + SAVE_FILE, jsonData);
    }

    public static PlayerSaveData LoadPlayer()
    {
        string jsonData;

        try
        {
            jsonData = File.ReadAllText(Application.persistentDataPath + "/" + SAVE_FILE);
        }
        catch (System.Exception e)
        {
            return null;
        }

        if (jsonData != null)
        {
            PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(jsonData);

            return playerSaveData;
        }

        return null;
    }
}
