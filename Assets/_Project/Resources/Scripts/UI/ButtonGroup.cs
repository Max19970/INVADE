using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
public class ButtonGroup : MonoBehaviour
{
    public bool shown = false;

    private CanvasGroup cGroup;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UIButton button = transform.GetChild(i).gameObject.GetComponent<UIButton>();

            if (button.gameObject.activeSelf) button.Show();
        }

        this.shown = true;
    }

    public void Hide()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UIButton button = transform.GetChild(i).gameObject.GetComponent<UIButton>();

            if (button.gameObject.activeSelf) button.Hide();
        }

        this.shown = false;
    }

    public void SetInteractable(bool interactable)
    {
        cGroup.interactable = interactable;
        cGroup.blocksRaycasts = interactable;

        for (int i = 0; i < transform.childCount; i++)
        {
            UIButton button = transform.GetChild(i).gameObject.GetComponent<UIButton>();

            if (button.gameObject.activeSelf) button.SetInteractable(interactable);
        }
    }
}
