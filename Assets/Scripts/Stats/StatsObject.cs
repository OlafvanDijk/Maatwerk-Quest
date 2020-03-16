using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StatsObject", menuName = "ScriptableObjects/Create StatsObject", order = 3)]
public class StatsObject : ScriptableObject
{
    public Stats stats;
}

[Serializable]
public class Stats {
    public int Vit;
    public int Wis;
    public int Str;
    public int Dex;
    public int Int;

    public Stats(int Vit, int Str, int Dex)
    {
        this.Vit = Vit;
        this.Str = Str;
        this.Dex = Dex;
    }

    public Stats(int Vit, int Wis, int Str, int Dex, int Int) : this(Vit, Str, Dex)
    {
        this.Wis = Wis;
        this.Int = Int;
    }

    /// <summary>
    /// Custom opperator so you can add up stats.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>Combined stats</returns>
    public static Stats operator +(Stats a, Stats b)
    {
        Stats newStat = new Stats(a.Vit, a.Wis, a.Str, a.Dex, a.Int);
        newStat.Vit += b.Vit;
        newStat.Wis += b.Wis;
        newStat.Str += b.Str;
        newStat.Dex += b.Dex;
        newStat.Int += b.Int;
        return newStat;
    }
}

/// <summary>
/// Class to sort by percentage and still know what stat it blongs to
/// </summary>
public class StatSort : IComparable
{
    public string name;
    public float percentage;

    public StatSort(string name, float percentage)
    {
        this.name = name;
        this.percentage = percentage;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        StatSort otherStat = obj as StatSort;
        if (otherStat != null)
        {
            return percentage.CompareTo(otherStat.percentage);
        }
        else
        {
            throw new Exception("Not the right type.");
        }
    }
}