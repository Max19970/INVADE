using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DentedPixel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;


public class UIButton : UIElement
{
    [Space(10)]
    [Header("Animation Timings")]
    public float idleAnimationTime = 1f;
    public float hoverAnimationTime = 0.1f;
    public float clickAnimationTime = 0.4f;

    [Space(10)]
    [Header("Scale Multipliers")]
    public float idleScaleMultiplier = 1f;
    public float hoverScaleMultiplier = 1.1f;
    public float clickScaleMultiplier = 0.9f;

    [Space(10)]
    [Header("Glow Values")]
    public bool withGlow;
    [Range(0, 1)] public float idleGlowValue = 0.5f;
    [Range(0, 1)] public float hoverGlowValue = 0.75f;
    [Range(0, 1)] public float clickGlowValue = 1f;

    [Space(10)]
    [Header("Button Functionality")]
    public Color blockedColor;
    public Color activeColor;

    [Space(10)]
    public UnityEvent onClick;
    public UnityEvent onStart;

    private TextMeshProUGUI m_TextMeshPro;
    private Vector3 initScale;
    private int scalingID;
    private int glowID;
    private int coloringID;

    protected override void Awake()
    {
        base.Awake();
        m_TextMeshPro = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        initScale = transform.localScale;
    }

    protected override void Start()
    {
        base.Start();
        Idle();
        onStart.Invoke();
    }

    private void Idle()
    {
        LeanTween.cancel(scalingID);
        scalingID = LeanTween.scale(gameObject, initScale * idleScaleMultiplier, idleAnimationTime).setLoopPingPong().id;
        if (withGlow)
        {
            LeanTween.cancel(glowID);
            glowID = LeanTween.value(gameObject,
            (float v) => { m_TextMeshPro.fontMaterial.SetFloat("_GlowOuter", v); },
            0, idleGlowValue, idleAnimationTime).setLoopPingPong().id;
        }
    }

    private void Hover() 
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Hover, transform.position);

        LeanTween.cancel(scalingID);
        scalingID = LeanTween.scale(gameObject, initScale * hoverScaleMultiplier, hoverAnimationTime).id;
        if (withGlow)
        {
            LeanTween.cancel(glowID);
            glowID = LeanTween.value(gameObject,
            (float v) => { m_TextMeshPro.fontMaterial.SetFloat("_GlowOuter", v); },
            m_TextMeshPro.fontMaterial.GetFloat("_GlowOuter"), hoverGlowValue, hoverAnimationTime).id;
        }
    }

    private void Unhover() 
    {
        LeanTween.cancel(scalingID);
        scalingID = LeanTween.scale(gameObject, initScale * idleScaleMultiplier, hoverAnimationTime).id;
        if (withGlow)
        {
            LeanTween.cancel(glowID);
            glowID = LeanTween.value(gameObject,
            (float v) => { m_TextMeshPro.fontMaterial.SetFloat("_GlowOuter", v); },
            m_TextMeshPro.fontMaterial.GetFloat("_GlowOuter"), 0, hoverAnimationTime).id;
        }
    }

    private void Click() 
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UIButton1Click, transform.position);

        LeanTween.cancel(scalingID);
        scalingID = LeanTween.scale(gameObject, initScale * clickScaleMultiplier, clickAnimationTime/2.0f).setOnComplete(() => 
        {
            scalingID = LeanTween.scale(gameObject, initScale, clickAnimationTime / 2.0f).setOnComplete(() => { Idle(); }).id; 
        }).id;

        if (withGlow)
        {
            LeanTween.cancel(glowID);
            glowID = LeanTween.value(gameObject,
            (float v) => { m_TextMeshPro.fontMaterial.SetFloat("_GlowOuter", v); },
            m_TextMeshPro.fontMaterial.GetFloat("_GlowOuter"), clickGlowValue, clickAnimationTime / 2.0f).setOnComplete(() =>
            {
                LeanTween.cancel(glowID);
                glowID = LeanTween.value(gameObject,
                    (float v) => { m_TextMeshPro.fontMaterial.SetFloat("_GlowOuter", v); },
                    m_TextMeshPro.fontMaterial.GetFloat("_GlowOuter"), 0, clickAnimationTime / 2.0f).id;
            }).id;
        }
    }

    protected override void Block()
    {
        LeanTween.cancel(coloringID);
        coloringID = LeanTween.value(gameObject, (Color v) => 
        {
            m_TextMeshPro.color = v;
        },
        m_TextMeshPro.color, blockedColor, hoverAnimationTime).id;
    }

    protected override void Unblock()
    {
        LeanTween.cancel(coloringID);
        coloringID = LeanTween.value(gameObject, (Color v) =>
        {
            m_TextMeshPro.color = v;
        },
        m_TextMeshPro.color, activeColor, hoverAnimationTime).id;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.DebugLog("Pointer click");
        Click();
        onClick.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.DebugLog("Pointer enter");
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        base.DebugLog("Pointer exit");
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.DebugLog("Selected");
        SetSelected(true);
        Hover();
    }

    public override void OnMove(AxisEventData eventData)
    {
        base.DebugLog("Moving");
        base.OnMove(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.DebugLog("Deselected");
        SetSelected(false);
        Unhover();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.DebugLog("Submitted");
        Click();
        if (base.selectOnSubmit != null) EventSystem.current.SetSelectedGameObject(base.selectOnSubmit.gameObject);
        onClick.Invoke();
    }

    public override void OnCancel(BaseEventData eventData)
    {
        base.DebugLog("Canceled");
        if (selected) Unhover();
        if (base.selectOnCancel != null) EventSystem.current.SetSelectedGameObject(base.selectOnCancel.gameObject);
    }
}