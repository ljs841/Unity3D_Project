using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_View : UIView
{
    public void OnLeftPress()
    {
        CreateEvent(eInputType.LeftPress);
    }

    public void OnRightPress()
    {

        CreateEvent(eInputType.RightPress);
    }

    public void OnLeftUp()
    {

        CreateEvent(eInputType.LeftUp);
    }

    public void OnRightUp()
    {

        CreateEvent(eInputType.RightUp);
    }

    void CreateEvent(eInputType type)
    {
        BattleInputEventArgs args = new BattleInputEventArgs();
        args.InputType = type;
        BattleManager._Instance.InputManager.AddInputEvent(args);
    }
}
