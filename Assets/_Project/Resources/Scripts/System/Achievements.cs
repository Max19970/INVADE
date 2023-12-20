using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Achievements : MonoBehaviour
{
    [Header("Global Presets")]
    public Transform achievementSpot;
    public GameObject achvUIPrefab;

    [Space(10)]
    [SerializeField] public List<Achievement> achievements = new List<Achievement>();

    public static Achievements instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Achievements object in the scene.");
        }
        instance = this;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.H)) GiveAchievement(0);
    // }

    public void GiveAchievement(int index)
    {
        achievements[index].have = 1;
        GameObject achievementUI = Instantiate(achvUIPrefab, achievementSpot.position, Quaternion.identity, achievementSpot.transform.parent);
        achievementUI.GetComponent<AchievementUI>().achievementIndex = achievements.IndexOf(achievements[index]);
    }
}


[Serializable]
public class Achievement
{
    public LocalizedString name;
    public LocalizedString description;
    public Sprite icon;
    private int have_data;
    public int have { get { return have_data; } set { this.have_data = value; Save(); } }
    private int seen_data;
    public int seen { get { return seen_data; } set { this.seen_data = value; Save(); } }
    public string savedataKey;

    public Achievement(LocalizedString name, LocalizedString description, Sprite icon, int have, int seen, string savedataKey)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.have = have;
        this.seen = seen;
        this.savedataKey = savedataKey;
    }

    public void Load()
    {
        have_data = (SaveSystem.HasKey($"{savedataKey}_Have", SaveManager.instance.settingsSlot) ? SaveSystem.Load<int>($"{savedataKey}_Have", SaveManager.instance.settingsSlot) : 0);
        seen_data = (SaveSystem.HasKey($"{savedataKey}_Seen", SaveManager.instance.settingsSlot) ? SaveSystem.Load<int>($"{savedataKey}_Seen", SaveManager.instance.settingsSlot) : 0);
    }

    public void Save()
    {
        SaveSystem.Save($"{savedataKey}_Have", have, SaveManager.instance.settingsSlot);
        SaveSystem.Save($"{savedataKey}_Seen", seen, SaveManager.instance.settingsSlot);
    }

    public void Reset()
    {
        have_data = 0;
        seen_data = 0;

        SaveSystem.Save($"{savedataKey}_Have", 0, SaveManager.instance.settingsSlot);
        SaveSystem.Save($"{savedataKey}_Seen", 0, SaveManager.instance.settingsSlot);
    }
}