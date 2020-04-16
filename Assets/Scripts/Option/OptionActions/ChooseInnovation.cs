using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseInnovation : Action
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<StatsObject> studies;

    public override void OnAction()
    {
        try
        {
            StatsObject study = studies.Find(item => item.stats.name.Equals(option.optionText));
            playerStats.studyStats = study;
            playerStats.CalculateStats();
            panelSwitcher.SwitchPanel(Panels.Main);
        }
        catch (System.Exception)
        {
            Debug.Log("Could not find an innovation named " + option.optionText);
        }
    }
}
