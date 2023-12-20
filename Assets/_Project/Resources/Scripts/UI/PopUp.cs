using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [Header("Global Presets")]
    public Fader background;
    public DialogWindow dialogWindow;

    [Header("Animation Presets")]
    [Space(10)]
    public float appearenceTime = 0.5f;

    private bool loaded;

    private void Start()
    {
        Show();
        loaded = true;
    }

    private void OnEnable()
    {
        if (loaded) Show();
    }

    public void Show()
    {
        background.Hide(appearenceTime);
        dialogWindow.Show();
    }

    public void Hide()
    {
        background.Show(appearenceTime);
        dialogWindow.Hide();
        LeanTween.value(gameObject, (float v) => { }, 0f, appearenceTime, appearenceTime).setOnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }
}
