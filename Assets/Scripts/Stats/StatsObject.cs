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
}
