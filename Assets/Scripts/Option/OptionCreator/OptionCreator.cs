using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionCreator : MonoBehaviour
{
    [SerializeField] private GameObject option;
    [SerializeField] private OptionList optionList;

    private void OnEnable()
    {
        CreateOptionsFromOptionList();
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Create and display a new option with an action.
    /// </summary>
    /// <param name="letterOrNumber">true for a letter, false for a number</param>
    /// <param name="optionText">The text that will be displayed for the option</param>
    /// <param name="action">The action that will be preformed when the option is selected</param>
    public void CreateNewOptionWithAction(bool letterOrNumber, string optionText, Type action)
    {
        GameObject firstOption = CreateNewOption(letterOrNumber, optionText, option);
        firstOption.AddComponent(action);
    }

    /// <summary>
    /// Create and display a new option.
    /// </summary>
    /// <param name="letterOrNumber">true for a letter, false for a number</param>
    /// <param name="optionText">The text that will be displayed for the option</param>
    /// <param name="optionPrefab">Prefab used to instantiate.</param>
    /// <returns>Gameobject instantiated</returns>
    public GameObject CreateNewOption(bool letterOrNumber, string optionText, GameObject optionPrefab)
    {
        GameObject firstOption = Instantiate(optionPrefab, this.transform) as GameObject;
        Option optionScript = firstOption.GetComponent<Option>();
        optionScript.SetOptionText(letterOrNumber, optionText);
        return firstOption;
    }

    /// <summary>
    /// Create options from list.
    /// </summary>
    public void CreateOptionsFromOptionList()
    {
        foreach (OptionListStruct item in optionList.options)
        {
            if (item.visible)
            {
                CreateNewOption(item.letterOrNumber, item.optionText, item.optionPrefab);
            }
        }
    }
}
