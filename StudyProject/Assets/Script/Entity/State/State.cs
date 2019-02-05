using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class State
{
  

    public State(int stateIdx)
    {
    }


    public virtual void OnEnter() { }
    public virtual void OnExcute() { }
    public virtual void OnExit() { }

    
}
