using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementContainer : MonoBehaviour
{
    public GameObject achievementPrefab;

    private void Start()
    {
        int i = 0;
        foreach (Achievement achv in Achievements.instance.achievements)
        {
            GameObject uiAchv = Instantiate(achievementPrefab, transform);
            uiAchv.GetComponent<ExtrasAchievement>().achievementIndex = i;
            i++;
        }
    }
}
