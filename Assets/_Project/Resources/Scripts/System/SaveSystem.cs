using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Serialization;

public static class SaveSystem
{
    public static int currentProfile = 0;

    public static void Save<T>(string key, T saveData, int profile)
    {
        //string jsonDataString=JsonUtility.ToJson(saveData,true);
        //string encryptedJsonData = jsonDataString.Encrypt();
        int startProfile = currentProfile;


        //PlayerPrefs.SetString(key, encryptedJsonData);
        ChangeProfile(profile);
        DataSerializer.Save(key, saveData);
        ChangeProfile(startProfile);
    }

    public static T Load<T>(string key, int profile) where T : new()
    {
        int startProfile = currentProfile;

        ChangeProfile(profile);
        if (DataSerializer.HasKey(key))
        {
            T loadedData = DataSerializer.Load<T>(key);
            //string decryptedLoader = loadedString.Decrypt();
            
            ChangeProfile(startProfile);
            //return JsonUtility.FromJson<T>(decryptedLoader);
            return loadedData;
        }
        else
        {
            ChangeProfile(startProfile);
            return new T();
        }
    }

    public static bool HasKey(string key, int profile)
    {
        int startProfile = currentProfile;

        ChangeProfile(profile);

        bool haskey = DataSerializer.HasKey(key);

        ChangeProfile(startProfile);

        return haskey;
    }

    public static bool CheckProfile(int id)
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, $"Save_{id}.data"));
    }

    public static void ChangeProfile(int id)
    {
        DataSerializer.ChangeProfile(id);
        currentProfile = id;
    }
}
