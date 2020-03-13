using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "ScriptableObjects/Create Area", order = 2)]
public class Area : ScriptableObject
{
    public string areaName;
    [Min(1)] public int minLevelEnemy = 1;
    [Min(1)] public int maxLevelEnemy = 1;
    [Min(1)] public int killAmount = 1;

    public List<GameObject> enemies = new List<GameObject>();
}