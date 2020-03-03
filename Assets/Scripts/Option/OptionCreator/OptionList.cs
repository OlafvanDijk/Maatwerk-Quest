using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Option", menuName = "ScriptableObjects/Create Option", order = 1)]
public class OptionList : ScriptableObject
{
    [SerializeField]
    public List<OptionListStruct> options;
}

[Serializable]
public struct OptionListStruct
{
    public string optionText;
    public bool letterOrNumber;
    public GameObject optionPrefab;
    public bool visible;
}