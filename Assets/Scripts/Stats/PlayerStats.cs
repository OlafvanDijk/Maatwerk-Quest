using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Create PlayerStats", order = 4)]
public class PlayerStats : ScriptableObject
{
    public StatsObject studyStats;
    public StatsObject innovationStats;
    public Stats selfAppointedStats;
    public Stats actualStats;

    public void CalculateStats()
    {
        if (studyStats != null && innovationStats != null)
        {
            actualStats = studyStats.stats + innovationStats.stats + selfAppointedStats;
            actualStats.CalculateStats();
        }
    }
}