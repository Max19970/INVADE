using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using FMODUnity;


[RequireComponent(typeof(Image))]
public class Diluter : MonoBehaviour
{
    [Header("Global Presets")]
    public bool hidden = false;
    public AnimationCurve animationCurve;

    [Header("SFX")]
    public EventReference soundOnHide;
    public EventReference soundOnShow;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Image>().material;
    }

    private void Start()
    {
        material.SetFloat("_Step", (hidden ? 0f : 1f));
    }

    public void Hide(float time)
    {
        if (soundOnHide.Guid != null) AudioManager.instance.PlayOneShot(soundOnHide, transform.position);

        LeanTween.value(gameObject, (float value) =>
        {
            material.SetFloat("_Step", value);
        },
        1f, 0f, time).setEase(animationCurve);

        this.hidden = true;
    }

    public void Show(float time)
    {
        if (soundOnShow.Guid != null) AudioManager.instance.PlayOneShot(soundOnShow, transform.position);

        LeanTween.value(gameObject, (float value) =>
        {
            material.SetFloat("_Step", value);
        },
        0f, 1f, time).setEase(animationCurve);

        this.hidden = false;
    }
}
