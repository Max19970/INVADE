using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using DentedPixel;


[RequireComponent(typeof(CanvasGroup))]
public class AchievementUI : MonoBehaviour
{
    [Header("Global Presets")]
    public int achievementIndex;

    [Space(10)]
    public Image background;
    public CanvasGroup innerCanvasGroup;
    public Image icon;
    public LocalizeStringEvent nameLocalization;
    public LocalizeStringEvent descriptionLocalization;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Load();
        Show();
    }

    public void Show()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.AchievementUnlock, transform.position);

        LeanTween.value(gameObject, (Color v) => { background.color = v; }, new Color(0f, 0f, 0f, 0f), new Color(1f, 1f, 1f, 1f), 0.2f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, (Color v) => { background.color = v; }, new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 1f), 0.2f);
            LeanTween.value(gameObject, (float v) => { innerCanvasGroup.alpha = v; }, 0f, 1f, 0.1f);
        });

        LeanTween.value(gameObject, (float v) => { }, 0f, 5f, 5f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, (float v) => { innerCanvasGroup.alpha = v; }, 1f, 0f, 0.1f).setOnComplete(() => 
            {
                LeanTween.value(gameObject, (float v) => { canvasGroup.alpha = v; }, 1f, 0f, 0.7f).setEase(LeanTweenType.easeOutCirc).setOnComplete(() => { Destroy(gameObject); });
            });
        });
    }

    private void Load()
    {
        if (Achievements.instance.achievements[achievementIndex].icon != null) icon.sprite = Achievements.instance.achievements[achievementIndex].icon;
        if (Achievements.instance.achievements[achievementIndex].name != null) nameLocalization.StringReference = Achievements.instance.achievements[achievementIndex].name;
        if (Achievements.instance.achievements[achievementIndex].description != null) descriptionLocalization.StringReference = Achievements.instance.achievements[achievementIndex].description;
    }
}
