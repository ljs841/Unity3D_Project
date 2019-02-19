using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_View : UIView
{

    public Slider _hpSlider;
    bool _leftPress = false;
    bool _rightPress = false;

    float _xDir = 0.0f;

    private void Start()
    {
       var _char = BattleManager._Instance.CurrentPlayer;
        _char.OnHpUpdate += HpUpdate;
    }

    public void HpUpdate(float value)
    {
        _hpSlider.value = value;
    }


    public void OnLeftPress()
    {
        _leftPress = true;
        SetMove(eInputType.LeftPress);
    }

    public void OnRightPress()
    {
        _rightPress = true;
        SetMove(eInputType.RightPress);
    }

    public void OnLeftUp()
    {
        _leftPress = false;
        SetMove(eInputType.LeftUp);
    }

    public void OnRightUp()
    {
        _rightPress = false;
        SetMove(eInputType.RightUp);
    }

    public void OnClickJump()
    {
        CreateEvent(eInputType.Jump);
    }

    public void OnClickCrouchPress()
    {
        CreateEvent(eInputType.Crouch);
    }

    public void OnClickCrouchUp()
    {
        CreateEvent(eInputType.None);
    }

    public void OnClickAttack()
    {
        CreateEvent(eInputType.Attak);
    }


    public void SetMove(eInputType type)
    {
        if (_leftPress)
        {
            _xDir = -1;
            type = eInputType.RightPress;
        }
        else if (_rightPress)
        {
            _xDir = 1;
            type = eInputType.LeftPress;
        }
        else
        {
            _xDir = 0;
            type = eInputType.NonMove;
        }
        CreateEvent(type);
    }
    void CreateEvent(eInputType type)
    {
        BattleInputEventArgs args = new BattleInputEventArgs();
       
        args.X_MoveDir = (int)_xDir;

        switch (type)
        {
            default:
                args.InputType = type;
                break;
        }

        BattleManager._Instance.InputManager.AddInputEvent(args);
    }
}
