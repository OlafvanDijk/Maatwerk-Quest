using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Option))]
public class Action : MonoBehaviour
{
    [HideInInspector] public Option option;
    [HideInInspector] public PanelSwitcher panelSwitcher;

    /// <summary>
    /// Get Option Component
    /// On ChooseOptionEvent invoke OnAction
    /// </summary>
    private void Awake()
    {
        option = GetComponent<Option>();
        option.ChooseOptionEvent.AddListener(OnAction);
    }

    /// <summary>
    /// Set the panelSwitcher
    /// </summary>
    private void Start()
    {
        panelSwitcher = PanelSwitcher.Instance;
    }

    /// <summary>
    /// Virtual OnAction method
    /// Defeault is to switch to the main panel
    /// </summary>
    public virtual void OnAction()
    {
        Debug.Log("Switch to main");
        panelSwitcher.SwitchPanel(Panels.Main);
    }

    /// <summary>
    /// Remove ChooseOptionEvent Listener
    /// </summary>
    private void OnDestroy()
    {
        option.ChooseOptionEvent.RemoveListener(OnAction);
    }
}
