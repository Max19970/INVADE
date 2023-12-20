using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIKeyControl : UIElement, ISaveable
{
    [Space(10)]
    [Header("Animation Timings")]
    public float hoverAnimationTime = 0.2f;

    [Space(10)]
    [Header("Key Control functionality")]
    public Image background;

    [Space(10)]
    public Color hoverColor;
    public Color unhoverColor;

    public override void OnCancel(BaseEventData eventData)
    {
        if (base.selectOnCancel != null) EventSystem.current.SetSelectedGameObject(base.selectOnCancel.gameObject);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }

    public void Save()
    {
        throw new System.NotImplementedException();
    }

    public void SetDefault()
    {
        throw new System.NotImplementedException();
    }

    protected override void Block()
    {
        throw new System.NotImplementedException();
    }

    protected override void Unblock()
    {
        throw new System.NotImplementedException();
    }
}