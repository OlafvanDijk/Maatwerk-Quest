using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStudy : Action
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<StatsObject> studies;

    public override void OnAction()
    {
        try
        {
            StatsObject study = studies.Find(item => item.stats.name.Equals(option.optionText));
            playerStats.studyStats = study;
            panelSwitcher.SwitchPanel(Panels.Innovation);
        }
        catch (System.Exception)
        {
            Debug.Log("Could not find a study named " + option.optionText);
        }
    }
}
