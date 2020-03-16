using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private StatsObject baseStats;
    [Space]
    [SerializeField] private float level;
    [SerializeField] private Stats actualStats;

    /// <summary>
    /// Set the stats based on level
    /// </summary>
    /// <param name="level">Current level</param>
    public void SetLevel(int level)
    {
        this.level = level;
        //Apply the base stats
        actualStats = new Stats(baseStats.stats.Vit, baseStats.stats.Str, baseStats.stats.Dex);

        //Calculate how many points to spend. If there are none then it returns
        int points = level - 1;
        if (points <= 0)
        {
            return;
        }

        //Calculate the total points of the stats so we can calculate the percantages.
        int totalstats = actualStats.Vit + actualStats.Str + actualStats.Dex;
        List<StatSort> percentages = new List<StatSort>() {
            new StatSort("Vit", (float)actualStats.Vit / totalstats),
            new StatSort("Str", (float)actualStats.Str / totalstats),
            new StatSort("Dex", (float)actualStats.Dex / totalstats)
        };

        //Sort the list and reverse it so that the highest percentage comes first
        percentages.Sort();
        percentages.Reverse();

        int vit = 0, str = 0, dex = 0;
        int pointsToSpend = points;
        int Count = percentages.Count;

        //To make sure that the biggest percentages are getting their cut of the points first I loop through the list and remove the highest each time after finishing.
        for (int i = 0; i < Count; i++)
        {
            switch (percentages[0].name)
            {
                case "Vit":
                    UpdatePoints(out vit, ref pointsToSpend, points, GetPercentage("Vit", percentages));
                    break;
                case "Str":
                    UpdatePoints(out str, ref pointsToSpend, points, GetPercentage("Str", percentages));
                    break;
                case "Dex":
                    UpdatePoints(out dex, ref pointsToSpend, points, GetPercentage("Dex", percentages));
                    break;
                default:
                    break;
            }
            percentages.Remove(percentages[0]);
        }

        //Apply the leveled up stats to the base stats
        actualStats.Vit += vit;
        actualStats.Str += str;
        actualStats.Dex += dex;
    }

    /// <summary>
    /// Updates point for the given stat
    /// </summary>
    /// <param name="stat">stat you want to update</param>
    /// <param name="pointsToSpend">points available to spend</param>
    /// <param name="totalPoints">total amount of points</param>
    /// <param name="percentage">Percentage of points that this stat will get</param>
    private void UpdatePoints(out int stat, ref int pointsToSpend, int totalPoints, float percentage)
    {
        if (pointsToSpend > 0)
        {
            stat = Mathf.CeilToInt(totalPoints * percentage);
            if (stat < 1)
            {
                stat = 1;
            }
            pointsToSpend -= stat;
        }
        else
        {
            stat = 0;
        }
    }

    /// <summary>
    /// Search in the list for the percentage of the stat we want.
    /// </summary>
    /// <param name="statName">Name of the stat</param>
    /// <param name="percentages">List we can find the stats in</param>
    /// <returns></returns>
    private float GetPercentage(string statName, List<StatSort> percentages)
    {
        return percentages.Find(m => m.name == statName).percentage;
    }
}