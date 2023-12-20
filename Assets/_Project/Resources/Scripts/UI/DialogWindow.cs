using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using DentedPixel;

public class DialogWindow : MonoBehaviour
{
    [Header("Global Presets")]
    public LocalizedString question;

    [Space(10)]
    public LocalizeStringEvent localization;
    public Diluter background;
    public CanvasGroup uiCanvas;

    [Header("Animation Timings")]
    public float appearenceTime = 0.5f;

    private void Start()
    {
        localization.StringReference = question;
    }

    public void Show()
    {
        background.Show(appearenceTime);
        LeanTween.value(gameObject, (float v) => { }, 0f, appearenceTime * 0.8f, appearenceTime * 0.8f).setOnComplete(() => 
        {
            uiCanvas.interactable = true;
            uiCanvas.blocksRaycasts = true;
            LeanTween.value(gameObject, (float v) => { uiCanvas.alpha = v; }, uiCanvas.alpha, 1f, appearenceTime * 0.2f);
        });
    }

    public void Hide()
    {
        background.Hide(appearenceTime);

        uiCanvas.interactable = false;
        uiCanvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float v) => { uiCanvas.alpha = v; }, uiCanvas.alpha, 0f, appearenceTime * 0.2f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, (float v) => { }, 0f, appearenceTime * 0.8f, appearenceTime * 0.8f);
        });
    }
}
