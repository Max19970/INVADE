using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using Unity.VisualScripting;

[RequireComponent(typeof(CanvasGroup))]
public class SubMenuTab : MonoBehaviour
{
    public bool shown;

    private CanvasGroup cGroup;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Show(0.2f);
    }

    public void Show(float time)
    {
        LeanTween.value(gameObject, (float value) =>
        {
            cGroup.alpha = value;
        },
        cGroup.alpha, 1f, time);

        this.shown = true;
    }

    public void Hide(float time)
    {
        LeanTween.value(gameObject, (float value) =>
        {
            cGroup.alpha = value;
        },
        cGroup.alpha, 0f, time);

        this.shown = false;
    }

    public void Open(float time)
    {
        Show(time);
        SetInteractable(true);
    }

    public void Close(float time) 
    {
        Hide(time);
        SetInteractable(false);
        LeanTween.value(gameObject, (float v) => { }, 0f, time, time).setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetInteractable(bool interactable)
    {
        foreach (UIElement element in transform.GetComponentsInChildren<UIElement>())
        {
            element.SetInteractable(interactable);
        }
    }
}
