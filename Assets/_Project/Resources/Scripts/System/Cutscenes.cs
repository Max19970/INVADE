using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscenes : MonoBehaviour
{
    [field: Header("Main Menu")]

    [field: SerializeField] public PlayableAsset MainMenuStart;
    [field: SerializeField] public PlayableAsset MenuAfterBegin;

    public static Cutscenes instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Cutscenes object in the scene.");
        }
        instance = this;
    }
}
