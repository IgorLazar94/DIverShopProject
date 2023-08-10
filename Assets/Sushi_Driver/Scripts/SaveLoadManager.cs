using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveData(SaveData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("GameData", jsonData);
        PlayerPrefs.Save();
    }

    public static SaveData LoadData()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            string jsonData = PlayerPrefs.GetString("GameData");
            return JsonUtility.FromJson<SaveData>(jsonData);
        }
        return null;
    }
}

[System.Serializable]
public class SaveData
{
    public int playerDefaultDollars;
    public bool isKitchenBuildComplete;
    public bool isShopBuildComplete;
    public bool isTrainingBuildComplete;
    // Добавьте другие поля для сохранения...
}
