using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UISlider : UIElement, ISaveable
{
    [Space(10)]
    [Header("Animation Timings")]
    public float hoverAnimationTime = 0.2f;

    [Space(10)]
    [Header("Slider functionality")]
    public Image background;
    public TextMeshProUGUI text;
    public Slider slider;

    [Space(10)]
    public Color hoverColor;
    public Color unhoverColor;

    [Space(10)]
    public float currentValue;
    public float defaultValue;
    public float deltaValue = 0.05f;

    [Space(10)]
    public UnityEvent onStart;
    public UnityEvent<float> onValueChange;

    [Space(10)]
    [Header("Save functionality")]
    public bool loadFromSave;
    public string savedataKey;

    protected override void Start()
    {
        base.Start();
        Load();
    }


    public void Hover() 
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Hover, transform.position);

        LeanTween.value(gameObject, (Color v) =>
        {
            background.color = v;
        },
        background.color, hoverColor, hoverAnimationTime);
    }

    public void Unhover() 
    {
        LeanTween.value(gameObject, (Color v) =>
        {
            background.color = v;
        },
        background.color, unhoverColor, hoverAnimationTime);
    }

    public void Load()
    {
        onStart.Invoke();

        if (SettingsManager.instance.floatSettings.ContainsKey(savedataKey) && loadFromSave)
        {
            float value = SettingsManager.instance.floatSettings[savedataKey];
            SetValue(value);
        }
        else
            SetDefault();
    }

    public void SetValue(float value)
    {
        slider.SetValueWithoutNotify(value);
        currentValue = value;
        SetValueText();
        onValueChange.Invoke(value);
    }

    public void SetValueText()
    {
        text.text = (currentValue * 100).ToString("0.00") + "%";
    }

    public override void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left || eventData.moveDir == MoveDirection.Right)
            SetValue(eventData.moveDir == MoveDirection.Left ? Mathf.Max(currentValue - deltaValue, 0) : Mathf.Min(currentValue + deltaValue, 1));
        else
            base.OnMove(eventData);
    }

    public override void OnCancel(BaseEventData eventData)
    {
        if (base.selectOnCancel != null) EventSystem.current.SetSelectedGameObject(base.selectOnCancel.gameObject);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        SetSelected(false);
        Unhover();
    }

    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData) { }

    public override void OnSelect(BaseEventData eventData)
    {
        SetSelected(true);
        Hover();
    }

    public override void OnSubmit(BaseEventData eventData) { }

    public void Save()
    {
        SaveSystem.Save(savedataKey, currentValue, SaveManager.instance.settingsSlot);
        SettingsManager.instance.floatSettings[savedataKey] = currentValue;
    }

    public void SetDefault()
    {
        SetValue(defaultValue);
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
