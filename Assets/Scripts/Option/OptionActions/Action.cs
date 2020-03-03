using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Option))]
public class Action : MonoBehaviour
{
    [HideInInspector]
    public Option option;

    void Awake()
    {
        option = GetComponent<Option>();
        option.ChooseOptionEvent.AddListener(OnAction);
    }

    public virtual void OnAction()
    {
        Debug.Log("Debug Action.");
    }

    private void OnDestroy()
    {
        option.ChooseOptionEvent.RemoveListener(OnAction);
    }
}
