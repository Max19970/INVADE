using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DentedPixel;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

public class UIChoice : UIElement, ISaveable
{
    [Space(10)]
    [Header("Animation Timings")]
    public float hoverAnimationTime;

    [Space(10)]
    [Header("Choice functionality")]
    public Image background;
    public TextMeshProUGUI choiceText;
    public UIArrow arrowBackward;
    public UIArrow arrowForward;

    [Space(10)]
    public Color hoverColor;
    public Color idleColor;

    [Space(10)]
    public bool localizeChoices;
    public int currentChoiceIndex;
    public List<string> choices = new List<string>();
    public List<LocalizedString> choiceLocalizations = new List<LocalizedString>();

    [Space(10)]
    public UnityEvent onStart;
    public OnValueChange onValueChange;

    [Space(10)]
    [Header("Save functionality")]
    public bool loadFromSave;
    public string savedataKey;

    [System.Serializable]
    public class OnValueChange : UnityEvent<int> { }

    [HideInInspector] public int defaultChoiceIndex;

    protected override void Awake()
    {
        base.Awake();
        defaultChoiceIndex = currentChoiceIndex;
    }

    protected override void Start()
    {
        base.Start();
        Load();
    }

    public void Load()
    {
        onStart.Invoke();

        if (SaveManager.instance != null && SettingsManager.instance.intSettings.ContainsKey(savedataKey) && loadFromSave)
        {
            int value = SettingsManager.instance.intSettings[savedataKey];
            ChangeChoiceDirect(value);
        }
        else
            SetDefault();
    }

    public void Save()
    {
        SaveSystem.Save(savedataKey, currentChoiceIndex, SaveManager.instance.settingsSlot);
        SettingsManager.instance.intSettings[savedataKey] = currentChoiceIndex;
    }

    private void Hover()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Hover, transform.position);

        LeanTween.value(gameObject, (Color v) =>
        {
            background.color = v;
        },
        background.color, hoverColor, hoverAnimationTime);
    }

    public void SetDefault() 
    {
        ChangeChoiceDirect(defaultChoiceIndex);
    }

    private void Unhover()
    {
        LeanTween.value(gameObject, (Color v) =>
        {
            background.color = v;
        },
        background.color, idleColor, hoverAnimationTime);
    }

    public void ChangeChoice(MoveDirection direction)
    {
        if (direction == MoveDirection.Left)
        {
            if (currentChoiceIndex != 0)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Click, transform.position);

                currentChoiceIndex--;
                arrowBackward.Go();
            }
        }
        else if (currentChoiceIndex != choices.Count - 1)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Click, transform.position);

            currentChoiceIndex++;
            arrowForward.Go();
        }

        SetChoiceText();
        onValueChange.Invoke(currentChoiceIndex);
    }

    public void ChangeChoiceDirect(int choiceIndex) 
    {
        currentChoiceIndex = choiceIndex;

        SetChoiceText();
        onValueChange.Invoke(currentChoiceIndex);
    }

    public void SetChoiceText() 
    {
        choiceText.text = choices[currentChoiceIndex];
        if (localizeChoices && choiceText != null) choiceText.gameObject.GetComponent<LocalizeStringEvent>().StringReference = choiceLocalizations[currentChoiceIndex];
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

    public override void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left || eventData.moveDir == MoveDirection.Right)
            ChangeChoice(eventData.moveDir);
        else
            base.OnMove(eventData);
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
