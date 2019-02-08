﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevInput : MonoBehaviour
{
    bool _leftPress = false;
    bool _rightPress = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            OnClickJump();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeftPress();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            OnLeftUp();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnRightPress(); 
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            OnRightUp();
        }

    }

    public void OnLeftPress()
    {
        _leftPress = true;
        CreateEvent(eInputType.LeftPress);
    }

    public void OnRightPress()
    {
        _rightPress = true;
        CreateEvent(eInputType.RightPress);
    }

    public void OnLeftUp()
    {
        _leftPress = false;
        CreateEvent(eInputType.LeftUp);
    }

    public void OnRightUp()
    {
        _rightPress = false;
        CreateEvent(eInputType.RightUp);
    }

    public void OnClickJump()
    {
        CreateEvent(eInputType.Jump);
    }

    void CreateEvent(eInputType type)
    {
        BattleInputEventArgs args = new BattleInputEventArgs();
        if (_rightPress == _leftPress)
        {
            args.InputType = eInputType.NonMove;
        }
        else
        {
            args.InputType = _leftPress ? eInputType.LeftPress : eInputType.RightPress;
        }

        if (type == eInputType.Jump)
        {
            args.InputType = type;
        }


        BattleManager._Instance.InputManager.AddInputEvent(args);
    }
}