using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : Action
{
    public override void OnAction()
    {
        string areaNumber = option.optionCharacter;
        Debug.Log("Enter Area " + areaNumber);
    }
}
