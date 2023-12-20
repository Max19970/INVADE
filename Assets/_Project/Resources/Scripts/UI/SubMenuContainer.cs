using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuContainer : MonoBehaviour 
{
    private void Update()
    {
        Debug.Log(((RectTransform)transform.parent).rect.height);
    }

    public void Reset()
    {
        foreach (ISaveable child in transform.GetComponentsInChildren<ISaveable>())
        {
            child.SetDefault();
        }
    }
}


public enum SCREEN_MODE
{
    BORDERLESS = 0,
    FULLSCREEN = 1,
    WINDOWED = 2,
}
