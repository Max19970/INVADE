using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using FMODUnity;


[RequireComponent(typeof(Image))]
public class Fader : MonoBehaviour
{
    [Header("Global Presets")]
    public bool faded = false;
    public AnimationCurve animationCurve;

    [Space(10)]
    public Color fadedColor;
    public Color unfadedColor;

    [Header("SFX")]
    public EventReference soundOnHide;
    public EventReference soundOnShow;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, (faded ? fadedColor.a : unfadedColor.a));
    }

    public void Hide(float time)
    {
        if (soundOnHide.Guid != null) AudioManager.instance.PlayOneShot(soundOnHide, transform.position);

        LeanTween.value(gameObject, (Color value) =>
        {
            image.color = value;
        },
        unfadedColor, fadedColor, time).setEase(animationCurve);

        this.faded = true;
    }

    public void Show(float time)
    {
        if (soundOnShow.Guid != null) AudioManager.instance.PlayOneShot(soundOnShow, transform.position);

        LeanTween.value(gameObject, (Color value) =>
        {
            image.color = value;
        },
        fadedColor, unfadedColor, time).setEase(animationCurve);

        this.faded = false;
    }
}
