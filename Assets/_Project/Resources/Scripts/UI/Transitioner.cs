using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
public class Transitioner : MonoBehaviour
{
    public GameObject prefab;

    public static Transitioner instance { get; private set; }

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("There are more than one Transitioner object in the scene.");
        }
        instance = this;
    }

    public void TransitionDown(float time)
    {
        GameObject transition = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        transition.transform.SetSiblingIndex(transform.childCount - 2);
        TransitionAnimations tAnims = transition.GetComponent<TransitionAnimations>();
        Fader tFader = transition.GetComponent<Fader>();
        ScreenExpander tExpander = transition.GetComponent<ScreenExpander>();

        tAnims.currentTransition = 0;

        tAnims.UpdateTransition();
        //tFader.Hide(time);
        tAnims.SetPosition(new Vector3(0f, 540f, 0f), time);
    }
}
