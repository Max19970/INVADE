using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Dev Settings")]
    [SerializeField] private bool debugMessages;

    // [Space(10)]
    // [SerializeField] public float menuTransitionTime = 3f;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one GameManager object in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
#if !UNITY_EDITOR
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
#endif
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.H))
    //         GiveAchievement(Achievements.instance.achievements[0]);
    // }
}
