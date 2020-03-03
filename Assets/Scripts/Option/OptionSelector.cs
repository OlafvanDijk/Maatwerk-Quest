using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionSelector : MonoBehaviour
{
    [SerializeField] FocusInputField inputField;

    public static OptionSelector Instance;

    private Dictionary<string, Option> options = new Dictionary<string, Option>();

    #region Letters & Numbers
    private string letters = "abcdefghijklmnopqrstuvwxyz";
    private string numbers = "123456789";

    private string unusedLetters;
    private string unusedNumbers;
    #endregion

    /// <summary>
    /// Create Instance or Destroy script if one already exists
    /// Set Unused sets
    /// </summary>
    void Awake()
    {
        if (OptionSelector.Instance == null)
        {
            Instance = this;
            unusedLetters = letters;
            unusedNumbers = numbers;
        }
        else
        {
            Debug.Log("An Option Selector already exists in the scene, this script will destroy itself", this.gameObject);
            Destroy(this);
        }
    }

    /// <summary>
    /// Execute when Submit has been pressed
    /// empties input field
    /// </summary>
    public void Submit()
    {
        ChooseOption(inputField.text);
        inputField.text = string.Empty;
    }

    private void ChooseOption(string input)
    {
        try
        {
            Option option = options[input];
            option.ChooseOption();
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("Option " + input + " does not exist");
        }
    }

    /// <summary>
    /// Gives option the first available letter
    /// </summary>
    /// <param name="option">Option Script</param>
    /// <returns>Letter that the option can be selected with</returns>
    public string GiveOptionALetter(Option option)
    {
        return AddOption(option, ref unusedLetters);
    }

    /// <summary>
    /// Gives option the first available number
    /// </summary>
    /// <param name="option">Option Script</param>
    /// <returns>Number that the option can be selected with</returns>
    public string GiveOptionANumber(Option option)
    {
        return AddOption(option, ref unusedNumbers);
    }

    /// <summary>
    /// Add Option to the dictionary
    /// </summary>
    /// <param name="option">Option Script</param>
    /// <param name="unusedSet">Set of unused characters</param>
    /// <returns>returns the first available option</returns>
    private string AddOption(Option option, ref string unusedSet)
    {
        string firstOption = GetFirstAvailableOption(ref unusedSet);
        if (firstOption != string.Empty)
        {
            options.Add(firstOption, option);
        }
        else
        {
            Debug.Log("No options available");
        }
        return firstOption;
    }

    /// <summary>
    /// Get's the first available option in the given set.
    /// </summary>
    /// <param name="unusedSet">Set of unused characters</param>
    /// <returns>returns the first available option</returns>
    private string GetFirstAvailableOption(ref string unusedSet)
    {
        if (unusedSet.Length >= 1)
        {
            string index = unusedSet.Substring(0, 1);

            if (unusedSet.Length > 1)
            {
                unusedSet = unusedSet.Substring(1);
            }
            else
            {
                unusedSet = string.Empty;
            }

            //Debug.Log(index + " " + unusedSet);
            return index;
        }
        return string.Empty;
    }

    /// <summary>
    /// Method to clear all the Letter options and reset the string
    /// </summary>
    public void ResetLetters()
    {
        ResetStringAndOptions(ref unusedLetters, ref letters);
    }

    /// <summary>
    /// Method to clear all the Number options and reset the string
    /// </summary>
    public void ResetNumbers()
    {
        ResetStringAndOptions(ref unusedNumbers, ref numbers);
    }

    /// <summary>
    /// Reset the referenced unused string and clear the dictionairy of the used set.
    /// </summary>
    /// <param name="unusedSet">unused character set</param>
    /// <param name="completeSet">compelte character set</param>
    public void ResetStringAndOptions(ref string unusedSet, ref string completeSet)
    {
        string used = completeSet.Substring(0, completeSet.IndexOf(unusedSet));
        for (int i = 0; i < used.Length; i++)
        {
            options.Remove(used[i].ToString());
        }
        unusedSet = completeSet;
    }
}
