using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DentedPixel;


[RequireComponent(typeof(CanvasGroup))]
public abstract class UIElement : Selectable, ISubmitHandler, ICancelHandler
{
    [Header("Dev Settings")]
    public bool enableDebugging;

    [Space(10)]
    [Header("UI Element Settings")]
    public float showAnimationTime = 1f;
    public float hideAnimationTime = 1f;
    [Range(0, 1)] public float showAnimationValue = 1f;
    [Range(0, 1)] public float hideAnimationValue = 0f;
    public bool selected;

    private bool blocked;

    [Header("Navigation")]
    public Selectable selectOnCancel;
    public Selectable selectOnSubmit;

    private CanvasGroup canvasGroup;

    private int showingID;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected abstract void Block();

    protected abstract void Unblock();

    public override abstract void OnPointerDown(PointerEventData eventData);

    public override abstract void OnPointerEnter(PointerEventData eventData);

    public override abstract void OnPointerExit(PointerEventData eventData);

    public override abstract void OnSelect(BaseEventData eventData);

    public override abstract void OnDeselect(BaseEventData eventData);

    public abstract void OnSubmit(BaseEventData eventData);

    public abstract void OnCancel(BaseEventData eventData);

    public void Show() 
    {
        LeanTween.cancel(showingID);
        showingID = LeanTween.value(gameObject, (float v) =>
        {
            canvasGroup.alpha = v;
        },
        canvasGroup.alpha, showAnimationValue, showAnimationTime).id;
    }

    public void Hide()
    {
        LeanTween.cancel(showingID);
        showingID = LeanTween.value(gameObject, (float v) =>
        {
            canvasGroup.alpha = v;
        },
        canvasGroup.alpha, hideAnimationValue, hideAnimationTime).id;
    }

    public void SetInteractable(bool interactable)
    {
        if (blocked) return;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }

    public void SetBlocked(bool blocked)
    {
        SetInteractable(!blocked);
        this.blocked = blocked;
        if (selected) EventSystem.current.SetSelectedGameObject(selectOnCancel.gameObject);
        if (blocked) Block();
        else Unblock();
    }

    protected void SetSelected(bool selected)
    {
        this.selected = selected;
    }

    public void DebugLog(string text)
    {
        if (enableDebugging) Debug.Log(text);
    }
}
