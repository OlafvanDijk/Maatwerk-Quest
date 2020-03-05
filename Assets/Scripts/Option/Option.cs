using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Option : MonoBehaviour
{
    public string optionCharacter;
    public string optionText;

    [SerializeField] private bool letterOrNumber;

    public UnityEvent ChooseOptionEvent;

    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
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
    /// Set the text field of the option by filling in the option text and option character.
    /// </summary>
    /// <param name="letterOrNumber">true for letter, false for number</param>
    /// <param name="optionText">Text that should be displayed for the option</param>
    public void SetOptionText(bool letterOrNumber,string optionText)
    {
        text = GetComponent<Text>();
        this.optionText = optionText;
        StartCoroutine(SetOptionCharacter(letterOrNumber));
    }

    /// <summary>
    /// Receives optionCharacter from the OptionSelector based on the letterOrNumber bool.
    /// </summary>
    /// <param name="letterOrNumber">true for letter, false for number</param>
    private IEnumerator SetOptionCharacter(bool letterOrNumber)
    {
        yield return new WaitUntil(() => OptionSelector.Instance != null);
        if (letterOrNumber)
        {
            optionCharacter = OptionSelector.Instance.GiveOptionALetter(this);
        }
        else
        {
            optionCharacter = OptionSelector.Instance.GiveOptionANumber(this);
        }
        text.text = this.ToString();
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
