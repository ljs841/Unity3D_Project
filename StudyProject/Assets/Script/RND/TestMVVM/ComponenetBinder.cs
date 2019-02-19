using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[Serializable]
public class ComponenetBinder
{
    [SerializeField]
    UIBehaviour _component;
    [SerializeField]
    string _propertyName;
    [SerializeField]
    string _commandName;
    [SerializeField]
    string _sourceUpdate;


    public UIBehaviour Component
    {
        get
        {
            return _component;
        }
    }

    public string PropertName
    {
        get
        {
            return _propertyName;
        }
    }

    public string CommandName
    {
        get
        {
            return _commandName;
        }
    }

    public string SourceUpdate
    {
        get
        {
            return _sourceUpdate;
        }
    }

    public void SetBinding(UIBehaviour obj , string propertyName)
    {

    }

}