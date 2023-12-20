using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class CameraAnimations : MonoBehaviour
{
    [Header("Initial Values")]
    public Vector3 initPosition;

    [Header("Animation Params")]
    public float setPositionTime = 2f;
    public AnimationCurve setPositionCurve;

    private void Start()
    {
        transform.localPosition = initPosition;
    }

    public void SetX(float pos_x) 
    {
        LeanTween.moveLocal(gameObject, new Vector3(pos_x, transform.position.y, transform.position.z), setPositionTime).setEase(setPositionCurve);
    }

    public void SetY(float pos_y)
    {
        LeanTween.moveLocal(gameObject, new Vector3(transform.position.x, pos_y, transform.position.z), setPositionTime).setEase(setPositionCurve);
    }

    public void SetZ(float pos_z)
    {
        LeanTween.moveLocal(gameObject, new Vector3(transform.position.x, transform.position.y, pos_z), setPositionTime).setEase(setPositionCurve);
    }
}
