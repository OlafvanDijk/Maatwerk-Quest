using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(OptionSelector))]
public class PanelSwitcher : MonoBehaviour
{
    public static PanelSwitcher Instance;
    [SerializeField]
    private List<PanelStruct> panels;

    private OptionSelector optionSelector;

    /// <summary>
    /// Create Instance or Destroy script if one already exists
    /// Set Unused sets
    /// </summary>
    void Awake()
    {
        if (PanelSwitcher.Instance == null)
        {
            Instance = this;
            optionSelector = GetComponent<OptionSelector>();
        }
        else
        {
            Debug.Log("A Panel Switcher already exists in the scene, this script will destroy itself", this.gameObject);
            Destroy(this);
        }
    }

    /// <summary>
    /// Switch panel to the panels with the given panelName.
    /// </summary>
    /// <param name="type">Type that a panel can have.</param>
    public void SwitchPanel(Panels type)
    {
        foreach (PanelStruct item in panels)
        {
            bool panelState = false;
            if (item.type.Equals(type))
            {
                if (item.panels.Count > 0 && !item.panels[0].activeSelf)
                {
                    optionSelector.ResetLettersAndNumbers();
                }
                panelState = true;
            }

            foreach (GameObject panel in item.panels)
            {
                panel.SetActive(panelState);
            }
        }
    }
}

[Serializable]
public struct PanelStruct
{
    public Panels type;
    public List<GameObject> panels;
}

public enum Panels
{
    Main,
    Area,
    Settings,
    Quit
}