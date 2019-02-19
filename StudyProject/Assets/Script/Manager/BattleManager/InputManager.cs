using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputManager
{
    public event EventHandler<BattleInputEventArgs> InputEvent;

    protected virtual void OnInputEvent(BattleInputEventArgs e)
    {


        EventHandler<BattleInputEventArgs> handler = InputEvent;
         handler?.Invoke(this, e);
    }

    public void AddInputEvent(BattleInputEventArgs e)
    {
        OnInputEvent(e);
    }
}


public class BattleInputEventArgs : EventArgs
{
   
    public eInputType InputType { get; set; }
    public int X_MoveDir { get; set; }
}