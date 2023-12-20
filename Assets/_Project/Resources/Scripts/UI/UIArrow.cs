using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DentedPixel;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class UIArrow : UIElement
{
    [Space(10)]
    [Header("Animation Timings")]
    public float goAnimationTime;
    public float blockAnimationTime;

    [Space(10)]
    [Header("Animation Parameters")]
    public float goPositionChange;

    [Space(10)]
    [Header("Arrow functionality")]
    public UIChoice parentChoice;
    public MoveDirection moveDirection;

    [Space(10)]
    public Color activeColor;
    public Color disabledColor;

    private new Image image;

    private int goID;

    private float posx;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

    protected override void Start()
    {
        base.Start();
        posx = transform.localPosition.x;
    }

    public void Go()
    {
        LeanTween.cancel(goID);
        goID = LeanTween.moveLocalX(gameObject, posx + goPositionChange, goAnimationTime/2f).setOnComplete(() => 
        {
            goID = LeanTween.moveLocalX(gameObject, posx, goAnimationTime / 2f).id;
        }
        ).id;
    }

    public override void OnCancel(BaseEventData eventData) { }

    public override void OnDeselect(BaseEventData eventData) { }

    public override void OnPointerDown(PointerEventData eventData)
    {
        parentChoice.ChangeChoice(moveDirection);
    }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    public override void OnSelect(BaseEventData eventData) { }

    public override void OnSubmit(BaseEventData eventData) { }

    protected override void Block()
    {
        LeanTween.value(gameObject, (Color v) =>
        {
            image.color = v;
        },
        image.color, disabledColor, blockAnimationTime);
    }

    protected override void Unblock()
    {
        LeanTween.value(gameObject, (Color v) =>
        {
            image.color = v;
        },
        image.color, activeColor, blockAnimationTime);
    }
}