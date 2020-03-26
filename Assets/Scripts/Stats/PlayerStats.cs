using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Create PlayerStats", order = 4)]
public class PlayerStats : ScriptableObject
{
    public CharacterClassStruct classStats;
    public CharacterRaceStruct raceStats;
    public Stats selfAppointedStats;
    public Stats actualStats;

    public void CalculateStats()
    {
        actualStats = classStats.classStats.stats + raceStats.raceStats.stats + selfAppointedStats;
        actualStats.CalculateStats();
    }
}

[Serializable]
public struct CharacterClassStruct
{
    public Classes characterClass;
    public StatsObject classStats;
}

[Serializable]
public struct CharacterRaceStruct
{
    public Races characterRace;
    public StatsObject raceStats;
}

public enum Classes
{
    Warrior,
    Ranger,
    Game_Designer
}

public enum Races
{
    Dwarf,
    Elf,
    Human
}