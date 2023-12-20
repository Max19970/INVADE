using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using DentedPixel;

public class ExtrasAchievement : MonoBehaviour
{
    [Header("Global Presets")]
    public int achievementIndex;

    [Space(10)]
    public Image icon;
    public LocalizeStringEvent nameLocalization;
    public LocalizeStringEvent descriptionLocalization;
    public Image shader;

    [Space(10)]
    public Color shaded;
    public Color unshaded;

    private bool loaded;

    private void Start()
    {
        Load();

        loaded = true;

        if (Achievements.instance.achievements[achievementIndex].have == 0) return;

        if (Achievements.instance.achievements[achievementIndex].seen == 0)
        {
            ShowOff();
            Achievements.instance.achievements[achievementIndex].seen = 1;
        }
        else
        {
            Show();
        }
    }

    private void OnEnable()
    {
        Debug.Log("enabled");

        if (loaded) 
        {
            Debug.Log(Achievements.instance.achievements[achievementIndex].seen);

            if (Achievements.instance.achievements[achievementIndex].have == 0) return;

            if (Achievements.instance.achievements[achievementIndex].seen == 0)
            {
                ShowOff();
                Achievements.instance.achievements[achievementIndex].seen = 1;
            }
            else Show();
        }
    }

    public void Show()
    {
        shader.color = unshaded;
    }

    public void ShowOff()
    {
        LeanTween.value(gameObject, (Color v) => { shader.color = v; }, shaded, unshaded, 0.5f).setEase(LeanTweenType.easeInCirc).setOnComplete(() => 
        {
            LeanTween.value(gameObject, (Color v) => { shader.color = v; }, new Color(1f, 1f, 1f, 1f), unshaded, 0.5f);
        });
    }

    private void Load()
    {
        if (Achievements.instance.achievements[achievementIndex].icon != null) icon.sprite = Achievements.instance.achievements[achievementIndex].icon;
        if (Achievements.instance.achievements[achievementIndex].name != null) nameLocalization.StringReference = Achievements.instance.achievements[achievementIndex].name;
        if (Achievements.instance.achievements[achievementIndex].description != null) descriptionLocalization.StringReference = Achievements.instance.achievements[achievementIndex].description;
    }
}
