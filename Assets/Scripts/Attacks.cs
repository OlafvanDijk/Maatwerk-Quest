using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AttackList", menuName = "ScriptableObjects/Create Attacks", order = 5)]
public class Attacks : ScriptableObject
{
    [SerializeField] List<AttackStruct> attacks;

    public AttackStruct GetAttack(string attackName)
    {
        return attacks.Find(item => item.name.Equals(attackName));    }
}

[Serializable]
public struct AttackStruct
{
    public string name;
    public int damage;
    public bool physicalOrMagic;
    public int manacost;
}