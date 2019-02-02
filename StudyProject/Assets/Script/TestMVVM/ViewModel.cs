using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel
{
    private Model _model;

    public Model Model
    {
        get
        {
            return Model;
        }
    }

   // private ModelUpdateCommand _command;

    public ViewModel()
    {
        _model = new Model();
        UpdateCommand = new ModelUpdateCommand();
    }
   

    public ICommand UpdateCommand
    {
        get;
        private set;
    }
}
