using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using DentedPixel;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)] public float masterVolume = 1;
    [Range(0, 1)] public float musicVolume = 1;
    [Range(0, 1)] public float ambienceVolume = 1;
    [Range(0, 1)] public float sfxVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    [Header("Tracks")]
    public TRACK_LAYER currentLayer = TRACK_LAYER.MENU;
    public List<FMODEventLayer> eventLayers = new List<FMODEventLayer>();

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one AudioManager object in the scene.");
        }
        instance = this;

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");

        for (int i = 0; i < 2; i++)
        {
            eventLayers.Add(new FMODEventLayer());
        }
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public FMODEventContainer CreateInstance(string name, EventReference sound, TRACK_LAYER layer)
    {
        if (eventLayers[(int)layer].Contains(name)) return eventLayers[(int)layer].Find(name);

        EventInstance eventInstance = RuntimeManager.CreateInstance(sound);
        FMODEventContainer container = new FMODEventContainer(name, eventInstance);

        eventLayers[(int)layer].Add(container);
        return container;
    }

    public void PlayLayer(TRACK_LAYER layer, bool muffleness, bool instantly)
    {
        eventLayers[(int)layer].Play();
    }

    public void SetPausedLayer(TRACK_LAYER layer, bool paused)
    {
        eventLayers[(int)layer].SetPaused(paused);
    }

    public void FocusOnLayer(TRACK_LAYER layer, float time)
    {
        for (int i = 0; i < eventLayers.Count; i++)
        {
            if (i != (int)layer)
                eventLayers[i].Muffle(time);
            else
                eventLayers[i].Unmuffle(time);
        }
    }

    public void ClearLayer(TRACK_LAYER layer)
    {
        for (int i = 0; i < eventLayers.Count; i++)
        {
            eventLayers[i].Clear();
        }
    }

    public void SetVolume(int id, float value)
    {
        switch (id) 
        {
            case 0:
                masterBus.setVolume(value);
                break;
            case 1:
                musicBus.setVolume(value);
                break;
            case 2:
                ambienceBus.setVolume(value);
                break;
            case 3:
                sfxBus.setVolume(value);
                break;
            default:
                break;
        }
    }
}


public class FMODEventLayer
{
    public List<FMODEventContainer> events;

    public FMODEventLayer(List<FMODEventContainer> events)
    {
        this.events = events;
    }

    public FMODEventLayer()
    {
        this.events = new List<FMODEventContainer>();
    }

    public void Play()
    {
        foreach (FMODEventContainer container in this.events)
        {
            container.Play();
        }
    }

    public void SetPaused(bool paused)
    {
        foreach (FMODEventContainer container in this.events)
        {
            container.SetPaused(paused);
        }
    }

    public void Unmuffle(float time)
    {
        foreach (FMODEventContainer container in this.events)
        {
            container.Unmuffle(time);
        }
    }

    public void Muffle(float time)
    {
        foreach (FMODEventContainer container in this.events)
        {
            container.Muffle(time);
        }
    }

    public void Add(FMODEventContainer container)
    {
        this.events.Add(container);
    }

    public bool Contains(string name)
    {
        foreach (FMODEventContainer container in this.events)
        {
            if (container.name == name) return true;
        }
        return false;
    }

    public FMODEventContainer Find(string name)
    {
        foreach (FMODEventContainer container in this.events)
        {
            if (container.name == name) return container;
        }
        return null;
    }

    public void Clear()
    {
        this.events = new List<FMODEventContainer>();
    }
}


public class FMODEventContainer
{
    public string name;
    public EventInstance eventInstance;

    public FMODEventContainer(string name, EventInstance eventInstance)
    {
        this.name = name;
        this.eventInstance = eventInstance;
    }

    public void Play()
    {
        eventInstance.start();
    }

    public void SetPaused(bool paused)
    {
        eventInstance.setPaused(paused);
    }

    public void Unmuffle(float time)
    {
        GameObject g = new GameObject();
        g.AddComponent<SelfDestroy>();
        g.GetComponent<SelfDestroy>().destroyTime = time;

        LeanTween.value(g, (float value) => 
        {
            eventInstance.setParameterByName("Muffle", value);
        },
        1f, 0f, time);
    }

    public void Muffle(float time)
    {
        GameObject g = new GameObject();
        g.AddComponent<SelfDestroy>();
        g.GetComponent<SelfDestroy>().destroyTime = time;

        LeanTween.value(g, (float value) => 
        {
            eventInstance.setParameterByName("Muffle", value);
        },
        0f, 1f, time);
    }
}


public enum TRACK_LAYER
{
    GAME = 0,
    MENU = 1,
}


public enum BUS
{
    MASTER = 0,
    MUSIC = 1,
    AMBIENCE = 2,
    SFX = 3,
}
