using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;


[RequireComponent(typeof(CanvasGroup))]
public class SubMenu : MonoBehaviour
{
    public bool shown;
    public bool switchAudio = true;

    public ButtonGroup buttons;

    private CanvasGroup cGroup;
    private SubMenuTab currentTab;

    private bool loaded = false;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Open(0.3f);
        loaded = true;
    }

    private void OnEnable()
    {
        if (loaded) Open(0.3f);
    }

    public void OpenTab(SubMenuTab tab)
    {
        if (currentTab == tab) return;

        currentTab?.Close(0.2f);

        tab.gameObject.SetActive(true);
        currentTab = tab;

        currentTab.Open(0.2f);
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
        if (switchAudio) AudioManager.instance.FocusOnLayer(TRACK_LAYER.MENU, time);
        Show(time);
        SetInteractable(true);
    }

    public void Close(float time)
    {
        if (switchAudio) AudioManager.instance.FocusOnLayer(TRACK_LAYER.GAME, time);
        Hide(time);
        SetInteractable(false);
        LeanTween.value(gameObject, (float v) => { }, 0f, time, time).setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetInteractable(bool interactable) 
    {
        cGroup.interactable = interactable;
        cGroup.blocksRaycasts = interactable;
        currentTab?.SetInteractable(interactable);
        buttons.SetInteractable(interactable);
    }
}
