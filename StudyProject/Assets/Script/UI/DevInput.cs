using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevInput : MonoBehaviour
{
    int _xDir = 0;
    int _yDir = 0;

    private void Update()
    {
#if UNITY_EDITOR

        OnMove();
        OnJump();
        OnAttack();
#endif

    }   

    void OnAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            CreateEvent( eInputType.Attak);
        }
    }

    void OnMove()
    {
        _xDir = (int)Input.GetAxisRaw("Horizontal");
        eInputType type;

        switch (_xDir)
        {
            case 1:
                type = eInputType.RightPress;
                break;
            case -1:
                type = eInputType.LeftPress;
                break;
            default:
                type = eInputType.NonMove;
                break;
        }


        CreateEvent(type);
    }
    
    void OnJump()
    {
        _yDir = (int)Input.GetAxisRaw("Vertical");
        eInputType type;
        switch (_yDir)
        {
            case 1:
                type = eInputType.Jump;
                break;
            case -1:
                type = eInputType.Crouch;
                break;
            default:
                type = eInputType.None;
                break;
        }
        CreateEvent(type);
    }

    public void OnClickJump()
    {
        CreateEvent(eInputType.Jump);
    }

    void CreateEvent(eInputType type)
    {
        BattleInputEventArgs args = new BattleInputEventArgs();       
        args.X_MoveDir = _xDir;
        switch (type)
        {         
            default:
                args.InputType = type;
                break;
        }


        BattleManager._Instance.InputManager.AddInputEvent(args);
    }
}
