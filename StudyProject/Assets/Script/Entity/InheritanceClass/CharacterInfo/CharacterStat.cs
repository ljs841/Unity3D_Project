using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    private string _name;
    private int _tempType;
    private float _maxHp;
    private float _maxMp;
    private float _baseAtK;
    private float _baseDef;
    private float _moveSpeed;
    private float _jumpForce;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public int TempType
    {
        get
        {
            return _tempType;
        }
    }
    public float MaxHp
    {
        get
        {
            return _maxHp;
        }
    }
    public float MaxMp
    {
        get
        {
            return _maxMp;
        }
    }
    public float BaseAtK
    {
        get
        {
            return _baseAtK;
        }
    }
    public float BaseDef
    {
        get
        {
            return _baseDef;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }
    public float JumpForce
    {
        get
        {
            return _jumpForce;
        }
    }

    public CharacterStat(string name , int type)
    {
        _name = name;
        _tempType = type;
    }

    public void SetMoveData(float moveSpeed, float jumpForce)
    {
        _moveSpeed = moveSpeed;
        _jumpForce = jumpForce;
    }

    public void SetMaxHpMpData(float maxHp , float maxMp)
    {
        _maxHp = maxHp;
        _maxMp = maxMp;
    }

    public void SetBattleData(float baseAtk , float baseDef)
    {
        _baseAtK = baseAtk;
        _baseDef = baseDef;
    }

}
