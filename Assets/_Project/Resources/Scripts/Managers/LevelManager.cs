using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using FMODUnity;

public class LevelManager : MonoBehaviour
{
    [field: Header("Global presets")]
    [field: SerializeField] public LEVEL Level { get; private set; }
    [field: SerializeField] public bool StartWithDefaults { get; private set; }

    [Space(10)]
    public TRACK_LAYER defaultLayer;
    public EventReference defaultMusic;
    public EventReference defaultAmbience;

    [Space(10)]
    public UnityEvent onStart;

    public static LevelManager instance { get; private set; }

    private PlayableDirector director;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one LevelManager object in the scene.");
        }
        instance = this;

        director = GetComponent<PlayableDirector>();
    }


    void Start()
    {
        if (StartWithDefaults)
        {
            AudioManager.instance.CreateInstance("Default Music", defaultMusic, defaultLayer);
            AudioManager.instance.CreateInstance("Default Ambience", defaultAmbience, defaultLayer);
            AudioManager.instance.PlayLayer(defaultLayer, false, true);
        }

        onStart.Invoke();
    }

    public void PlayCutscene(PlayableAsset cutscene)
    {
        director.playableAsset = cutscene;
        director.Play();
    }

    public void EnableG(GameObject g)
    {
        g.SetActive(true);
    }

    public void DisableG(GameObject g)
    {
        g.SetActive(false);
    }

    public void GiveAchievement(int index) 
    {
        Achievements.instance.GiveAchievement(index);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("End!");
    }
}
