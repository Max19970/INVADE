using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference MainTheme { get; private set; }
    [field: SerializeField] public EventReference NightmareInTheLocker { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference CampfireWithWind { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference UIButton1Hover { get; private set; }
    [field: SerializeField] public EventReference UIButton1Click { get; private set; }
    [field: SerializeField] public EventReference EchoBlub { get; private set; }
    [field: SerializeField] public EventReference AchievementUnlock { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEvents object in the scene.");
        }
        instance = this;
    }
}
