using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    public string optionCharacter;
    [SerializeField] private string optionText;
    [SerializeField] private bool letterOrNumber = true;

    public UnityEvent ChooseOptionEvent;

    /// <summary>
    /// Receives optionCharacter from the OptionSelector
    /// </summary>
    void Start()
    {
        if (letterOrNumber)
        {
            optionCharacter = OptionSelector.Instance.GiveOptionALetter(this);
        }
        else
        {
            optionCharacter = OptionSelector.Instance.GiveOptionANumber(this);
        }
    }

    /// <summary>
    /// Called from the OptionSelector
    /// Invokes ChooseOptionEvent
    /// </summary>
    public void ChooseOption()
    {
        Debug.Log("Player choose option: " + ToString(), this.gameObject);
        ChooseOptionEvent.Invoke();
    }

    /// <summary>
    /// Displays the Option Character + the option text
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return optionCharacter + ") " + optionText;
    }
}
