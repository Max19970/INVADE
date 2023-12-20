using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDefault : UIElement
{
    public static UIDefault instance { get; private set; }

    private new void Awake()
    {
        base.Awake();

        if (instance != null) 
        {
            base.DebugLog("There is more than one UI Default object in the scene.");
        }
        instance = this;
    }

    private void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    protected override void Block() { }

    protected override void Unblock() { }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
    }

    public override void OnCancel(BaseEventData eventData) { }

    public override void OnDeselect(BaseEventData eventData) 
    {
        base.SetInteractable(false);
        base.SetSelected(false);
    }

    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    public override void OnSelect(BaseEventData eventData) 
    {
        base.SetInteractable(true);
        base.SetSelected(true);
    }

    public override void OnSubmit(BaseEventData eventData) { }
}