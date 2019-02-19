using System;
using System.Collections.Generic;

public class Model : INotyPropertyChange
{
    private int _id;
    private string _name;
    public int ID
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
            OnPropertyChange();
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            OnPropertyChange();
        }
    }

    public Model()
    {
        _id = 33;
        _name = "asd";
    }

    public event EventHandler PropertyChange;


    private void OnPropertyChange()
    {
        PropertyChange(this, null);
    }

}