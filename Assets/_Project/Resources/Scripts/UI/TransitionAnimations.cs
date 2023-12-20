using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.Events;
using UnityEngine.UI;

public class TransitionAnimations : MonoBehaviour
{
    [Header("Global Presets")]
    public int currentTransition = 0;
    public List<Sprite> transitions = new List<Sprite>();

    private Image image;

    private List<Vector3> positionInits = new List<Vector3>() 
    {
        new Vector3(0f, -1620f, -1f),
    };

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        UpdateTransition();
    }

    public void UpdateTransition()
    {
        transform.localPosition = positionInits[currentTransition];
        image.sprite = transitions[currentTransition];
    }

    public void SetPosition(Vector3 pos, float time)
    {
        LeanTween.moveLocal(gameObject, pos, time);
    }

    public void SetX(float pos_x, float time)
    {
        LeanTween.moveLocal(gameObject, new Vector3(pos_x, transform.position.y, transform.position.z), time);
    }

    public void SetY(float pos_y, float time)
    {
        LeanTween.moveLocal(gameObject, new Vector3(transform.position.x, pos_y, transform.position.z), time);
    }

    public void SetZ(float pos_z, float time)
    {
        LeanTween.moveLocal(gameObject, new Vector3(transform.position.x, transform.position.y, pos_z), time);
    }

    public void DestroySelf(float time)
    {
        Destroy(gameObject, time);
    }
}
