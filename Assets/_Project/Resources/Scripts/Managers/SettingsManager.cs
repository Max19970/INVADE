using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour 
{
    public Dictionary<string, int> intSettings = new Dictionary<string, int>();
    public Dictionary<string, float> floatSettings = new Dictionary<string, float>();

    public bool loaded;

    public static SettingsManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one SettingsManager object in the scene.");
        }
        instance = this;
    }

    public void Load()
    {
        StartCoroutine(LoadLocale());
        LoadScreen();
        LoadVolume();

        loaded = true;
    }

    public void SetLocale(int id) 
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        SaveSystem.Save("selectedLocale", id, SaveManager.instance.settingsSlot);
    }

    public IEnumerator LoadLocale() 
    {
        int locale = 1;

        while (LocalizationSettings.Instance.GetAvailableLocales().Locales.Count < 2) 
        {
            yield return null;
        }

        if (SaveSystem.HasKey("selectedLocale", SaveManager.instance.settingsSlot))
        {
            locale = SaveSystem.Load<int>("selectedLocale", SaveManager.instance.settingsSlot);
        }

        SetLocale(locale);
        intSettings.Add("selectedLocale", locale);
    }

    public void LoadScreen() 
    {
        int resolutionIndex = 0;
        int screenMode = 0;
        int fps = 2;
        int runInBack = 1;

        if (SaveSystem.HasKey("screenResolution", SaveManager.instance.settingsSlot))
        {
            resolutionIndex = SaveSystem.Load<int>("selectedLocale", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("screenMode", SaveManager.instance.settingsSlot)) 
        {
            screenMode = SaveSystem.Load<int>("screenMode", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("fpsCap", SaveManager.instance.settingsSlot))
        {
            fps = SaveSystem.Load<int>("fpsCap", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("runInBack", SaveManager.instance.settingsSlot)) 
        {
            runInBack = SaveSystem.Load<int>("runInBack", SaveManager.instance.settingsSlot);
        }

        if (resolutionIndex > 0)
            Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, DefineScreenMode(screenMode));
        else 
        {
            int i = 0;
            foreach (Resolution res in Screen.resolutions)
            {
                if (Screen.currentResolution.width == res.width && Screen.currentResolution.height == res.height) resolutionIndex = i;
                i++;
            }

            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, DefineScreenMode(screenMode));
        }

        intSettings.Add("screenResolution", resolutionIndex);
        intSettings.Add("screenMode", screenMode);
        intSettings.Add("fpsCap", fps);
        intSettings.Add("runInBack", runInBack);

        QualitySettings.vSyncCount = DefineVsync(fps);
        Application.targetFrameRate = DefineFPS(fps);
        SetRunInBackground(runInBack);
    }

    public void LoadVolume() 
    {
        float masterVolume = 1f;
        float musicVolume = 1f;
        float ambienceVolume = 1f;
        float sfxVolume = 1f;

        if (SaveSystem.HasKey("masterVolume", SaveManager.instance.settingsSlot))
        {
            masterVolume = SaveSystem.Load<float>("masterVolume", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("musicVolume", SaveManager.instance.settingsSlot))
        {
            musicVolume = SaveSystem.Load<float>("musicVolume", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("ambienceVolume", SaveManager.instance.settingsSlot))
        {
            ambienceVolume = SaveSystem.Load<float>("ambienceVolume", SaveManager.instance.settingsSlot);
        }
        if (SaveSystem.HasKey("sfxVolume", SaveManager.instance.settingsSlot))
        {
            sfxVolume = SaveSystem.Load<float>("sfxVolume", SaveManager.instance.settingsSlot);
        }

        floatSettings.Add("masterVolume", masterVolume);
        floatSettings.Add("musicVolume", musicVolume);
        floatSettings.Add("ambienceVolume", ambienceVolume);
        floatSettings.Add("sfxVolume", sfxVolume);

        AudioManager.instance.SetVolume((int)BUS.MASTER, masterVolume);
        AudioManager.instance.SetVolume((int)BUS.MUSIC, musicVolume);
        AudioManager.instance.SetVolume((int)BUS.AMBIENCE, ambienceVolume);
        AudioManager.instance.SetVolume((int)BUS.SFX, sfxVolume);
    }

    public FullScreenMode DefineScreenMode(int id)
    {
        switch (id)
        {
            case 1:
                return FullScreenMode.ExclusiveFullScreen;
            case 2:
                return FullScreenMode.Windowed;
            default:
                return FullScreenMode.FullScreenWindow;
        }
    }

    public int DefineVsync(int id)
    {
        if (id == 0) return 1;
        return 0;
    }

    public int DefineFPS(int id)
    {
        switch (id)
        {
            case 1:
                return 30;
            case 2:
                return 60;
            case 3:
                return 120;
            case 4:
                return 144;
            case 5:
                return -1;
            default:
                return 60;
        }
    }

    public void SetRunInBackground(int id) 
    {
        Application.runInBackground = id == 0;
    }

    public void SetBrightness(float value)
    {
        Screen.brightness = value;
    }

    public void SetVolume(int id, float volume) 
    {
        AudioManager.instance.SetVolume(id, volume);
    }
}
