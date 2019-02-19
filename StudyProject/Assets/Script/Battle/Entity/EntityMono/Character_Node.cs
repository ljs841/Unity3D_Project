using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Node : MonoBehaviour
{
    [SerializeField]
    private Transform _top;
    public Transform Top
    {
        get
        {
            return _top;
        }
    }

    [SerializeField]
    private Transform _middle;
    public Transform Middle
    {
        get
        {
            return _middle;
        }
    }

    [SerializeField]
    private Transform _bottom;
    public Transform Bottom
    {
        get
        {
            return _bottom;
        }
    }


    [SerializeField]
    private Transform _meleeAttackPos;
    public Transform MeleeAttackPos
    {
        get
        {
            return _meleeAttackPos;
        }
    }

    [SerializeField]
    private Vector2 _forwardCheckRectSize;
    public Vector2 ForwardCheckRectSize
    {
        get
        {
            return _forwardCheckRectSize;
        }
    }

    [SerializeField]
    private Vector2 _meleeCheckRectSize;
    public Vector2 MeleeCheckRectSize
    {
        get
        {
            return _meleeCheckRectSize;
        }
    }

    


    [SerializeField]
    private Transform _rangeAttackNode;
    public Transform RangeAttackNode
    {
        get
        {
            return _rangeAttackNode;
        }
    }


    public void Init()
    {

    }


}
