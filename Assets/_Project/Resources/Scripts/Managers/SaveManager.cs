using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class SaveManager : MonoBehaviour
{
    public int settingsSlot = 21;
    public static SaveManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one SaveManager object in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        SettingsManager.instance.Load();
        LoadAchievements();
    }

    public void LoadAchievements() 
    {
        foreach (Achievement achv in Achievements.instance.achievements)
        {
            achv.Load();
            Debug.Log($"Achievement loaded: Name: {achv.name}; Have: {achv.have}; Seen: {achv.seen}");
        }
    }
}

public enum LEVEL
{
    MAIN_MENU = 0,
    CH_PROLOGUE = 1,
    CH_ONE = 2,
    CH_TWO = 3,
    CH_THREE = 4,
    CH_FOUR = 5,
    CH_FIVE = 6,
    CH_SIX = 7,
    CH_SEVEN = 8,
    CH_EPILOGUE = 9,
}
