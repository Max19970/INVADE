using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class UIStuff : MonoBehaviour
{
    public void BlockButtonIfNotPlayedYet()
    {
        GetComponent<UIButton>().SetBlocked(!SaveSystem.HasKey("lastPlayedSave", SaveManager.instance.settingsSlot));
    }

    public void OpenSubmenu(GameObject obj)
    {
        obj.SetActive(true);
    }

    // public void SnapScrollToObject(GameObject obj)
    // {
    //     if ()
    // }

    public void GetResolutions(UIChoice choicer)
    {
        int i = 0;
        foreach (Resolution res in Screen.resolutions)
        {
            choicer.choices.Add(res.width + "x" + res.height);
            if (Screen.currentResolution.width == res.width && Screen.currentResolution.height == res.height) choicer.defaultChoiceIndex = i;
            i++;
        }
    }

    public void ChangeLocale(int id) 
    {
        SettingsManager.instance.SetLocale(id);
    }

    public void ApplyResSettings()
    {
        int resolutionIndex = SettingsManager.instance.intSettings["screenResolution"];
        FullScreenMode screenMode = SettingsManager.instance.DefineScreenMode(SettingsManager.instance.intSettings["screenMode"]);
        int vsync = SettingsManager.instance.DefineVsync(SettingsManager.instance.intSettings["fpsCap"]);
        int fps = SettingsManager.instance.DefineFPS(SettingsManager.instance.intSettings["fpsCap"]);

        Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, screenMode);
        QualitySettings.vSyncCount = vsync;
        Application.targetFrameRate = fps;
    }

    public void SetRunInBackground(int id)
    {
        SettingsManager.instance.SetRunInBackground(id);
    }

    public void GetBrightness(UISlider slider)
    {
        slider.currentValue = Screen.brightness;
    }

    public void SetBrightness(float value)
    {
        SettingsManager.instance.SetBrightness(value);
    }

    public void SetMasterVolume(float value)
    {
        SettingsManager.instance.SetVolume((int)BUS.MASTER, value);
    }

    public void SetMusicVolume(float value)
    {
        SettingsManager.instance.SetVolume((int)BUS.MUSIC, value);
    }

    public void SetAmbienceVolume(float value)
    {
        SettingsManager.instance.SetVolume((int)BUS.AMBIENCE, value);
    }

    public void SetSFXVolume(float value)
    {
        SettingsManager.instance.SetVolume((int)BUS.SFX, value);
    }
}
