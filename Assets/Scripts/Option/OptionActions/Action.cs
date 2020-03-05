using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Option))]
public class Action : MonoBehaviour
{
    [HideInInspector] public Option option;
    [HideInInspector] public PanelSwitcher panelSwitcher;

    void Awake()
    {
        option = GetComponent<Option>();
        option.ChooseOptionEvent.AddListener(OnAction);
    }

    private void Start()
    {
        panelSwitcher = PanelSwitcher.Instance;
    }

    public virtual void OnAction()
    {
        Debug.Log("Switch to main");
        panelSwitcher.SwitchPanel(Panels.Main);
    }

    private void OnDestroy()
    {
        option.ChooseOptionEvent.RemoveListener(OnAction);
    }
}
