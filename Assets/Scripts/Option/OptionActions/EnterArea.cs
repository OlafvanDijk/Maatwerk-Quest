using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : Action
{
    public override void OnAction()
    {
        string area = option.optionText;
        PlayerPrefs.SetString("Area", area);
        panelSwitcher.SwitchPanel(Panels.Area);
        Debug.Log("Enter Area " + area);
    }
}
