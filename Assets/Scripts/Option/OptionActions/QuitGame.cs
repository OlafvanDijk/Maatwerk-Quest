using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : Action
{
    /// <summary>
    /// Quits game
    /// </summary>
    public override void OnAction()
    {
        //TODO Quit Dialog Y/N
        Debug.Log("Quit Game");
        //panelSwitcher.SwitchPanel(Panels.Quit);
    }
}
