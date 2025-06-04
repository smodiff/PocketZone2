using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem
{
    public static void SavePlayer(Player player)
    {

        string path = Application.persistentDataPath + "/player.txt";

        PlayerData data = new PlayerData(player);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static void DeletePlayerData()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            Debug.Log("Save file has been deleted");
            File.Delete(path);
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            string save = File.ReadAllText(path);

            Debug.Log(save);

            PlayerData data = JsonUtility.FromJson<PlayerData>(save);

            Debug.Log(data.PlayerInventory);

            return data;
        }
        else
        {
            Debug.Log("Save not found!");
            return null;
        }
    }
}
