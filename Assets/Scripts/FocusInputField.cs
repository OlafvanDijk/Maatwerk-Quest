using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusInputField : InputField
{
    /// <summary>
    /// Select on Start
    /// </summary>
    private void Start()
    {
        ActivateInputField();
    }

    /// <summary>
    /// Do nothing when deselecting so the field stays focussed.
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDeselect(BaseEventData eventData)
    {
        //Do Nothing
    }
}
