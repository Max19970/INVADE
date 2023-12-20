using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScreenExpander : MonoBehaviour
{
    public Canvas canvas;

    private void Update()
    {
        if (canvas != null)
        {
            RectTransform canvasRect = canvas.gameObject.GetComponent<RectTransform>();
            float canvasRatio = canvasRect.sizeDelta.x/canvasRect.sizeDelta.y;
            RectTransform myRect = GetComponent<RectTransform>();
            float myRatio = 16/9f;

            if (canvasRatio > myRatio)
            {
                myRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x, 1080*(canvasRect.sizeDelta.x/1920));
            }
            else
            {
                myRect.sizeDelta = new Vector2(1920*(canvasRect.sizeDelta.y/1080), canvasRect.sizeDelta.y);
            }
        }
    }
}
