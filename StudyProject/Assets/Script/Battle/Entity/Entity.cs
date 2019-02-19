using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour 
{
    protected eEntityType _type = eEntityType.None;
    public eEntityType Type
    {
        get
        {
            return _type;
        }

    }
    
    protected SpriteAnimationController _aniControl;
    public SpriteAnimationController AniControl
    {
        get
        {
            return _aniControl;
        }
        set
        {
            _aniControl = value;
            _aniControl.Init();
        }
    }


    protected SimplePhysics _physics;

    protected bool _isActive;
    public bool IsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            if(value == true)
            {
                ActiveBehavior();
            }
            else
            {
                DeActiveBehavior();
            }
            _isActive = value;
        }
    }

    protected eEntityLookDir _baseLookDir = eEntityLookDir.None;
    public eEntityLookDir BaseLookDir
    {
        get
        {
            return _baseLookDir;
        }
    }

    public Vector2 Forward
    {
        get
        {
            return _physics.Forward;
        }

    }

    protected eUnitAllyType _allyType;
    public eUnitAllyType AllyType
    {
        get
        {
            return _allyType;
        }
        set
        {
            _allyType = value;
        }
    }
    protected Character_Node _node;
    public Character_Node Node
    {
        get
        {
            return _node;
        }
        set
        {
            _node = value;
        }
    }
    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody
    {
        get
        {
            return _rigidbody;
        }
        set
        {
            _rigidbody = value;
        }
    }


    protected float _logicIntevalSec = 0.0f;
    protected WaitForSeconds _waitLogicInteval;

    public virtual void Init(eEntityType type, eEntityLookDir baseDir, int subType)
    {
        _type = type;
        _baseLookDir = baseDir;
        _logicIntevalSec = Util.LogicInterval(type);
        _waitLogicInteval = new WaitForSeconds(_logicIntevalSec);
    }
    public virtual void FixedUpdateEntity() { }
    public virtual void UpdateEntity() { }
    public virtual void MovePosition() { }

    public virtual float GetEntSpeeditySpeed() { return 0.0f; }

    public virtual void ActiveBehavior()
    {
        _aniControl.SpriteRenderer.enabled = true;
        gameObject.SetActive(true);
    }

    public virtual void DeActiveBehavior()
    {

        _aniControl.SpriteRenderer.enabled = false;
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    public void SetForward(Vector2 vec)
    {
        vec.Normalize();
        _physics.SetForward(vec);
        _aniControl.SetSpriteFlip(_baseLookDir, vec);
    }
    protected bool isStart = false;
    public virtual void StartEntity()
    {
        isStart = true;
        StartCoroutine(coUpdateEntity());
    }

    protected virtual IEnumerator coUpdateEntity()
    {
        while (true)
        {
            UpdateEntity();
            yield return null;
        }
    }

}