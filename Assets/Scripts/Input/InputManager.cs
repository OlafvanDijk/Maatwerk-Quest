using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] FocusInputField inputField;

    public UnityEvent SubmitEvent;
    public UnityEvent EscapeEvent;

    /// <summary>
    /// Set cursor visibility to false
    /// </summary>
    private void Start()
    {
        Cursor.visible = false;
    }

    /// <summary>
    /// Submit Event
    /// Set focus on the input field.
    /// </summary>
    /// <param name="context"></param>
    public void Submit(InputAction.CallbackContext context)
    {
        if (CheckPerformed(context))
        {
            SubmitEvent.Invoke();
        }
        SetFocusOnInputField();
    }

    /// <summary>
    /// Escape Event
    /// Quits application
    /// </summary>
    /// <param name="context"></param>
    public void Escape(InputAction.CallbackContext context)
    {
        if (CheckPerformed(context))
        {
            EscapeEvent.Invoke();
            Debug.Log("Escape");
        }
    }

    /// <summary>
    /// Check if the InputAction has been performed
    /// This method is used to prevent an action to be executed multiple times in a row.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private bool CheckPerformed(InputAction.CallbackContext context)
    {
        return context.performed;
    }

    /// <summary>
    /// Set Focus on InputField
    /// </summary>
    private void SetFocusOnInputField()
    {
        if (inputField != null)
        {
            inputField.ActivateInputField();
        }
    }
}
