using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity 
{
    protected eEntityType _type = eEntityType.None;

    public eEntityType Type
    {
        get
        {
            return _type;
        }

    }

    protected EntityBehaviour _behaviour;
    public EntityBehaviour Behaviour
    {
        get
        {
            return _behaviour;
        }

        set
        {
            _behaviour = value;
        }
    }

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
                _behaviour.ActiveBehavior(_currentLookDir);
            }
            else
            {
                _behaviour.DeActiveBehavior();
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
    protected eEntityLookDir _currentLookDir = eEntityLookDir.None;
    public eEntityLookDir CurrentLookDir
    {
        get
        {
            return _currentLookDir;
        }
    }


    public Entity(eEntityType type , eEntityLookDir baseDir ,  int subType)
    {
        _type = type;
        _baseLookDir = baseDir;
        _currentLookDir = _baseLookDir;
    }

    protected virtual EntityBehaviourData SetBehaviourData()
    {
        return null;
    }
    public virtual void Init(EntityBehaviour mono)
    {
        _behaviour = mono;
        _behaviour.Init();
        _behaviour.SetEntityBehaviourData(SetBehaviourData());
    }


}

public class EntityBehaviourData
{
    public eEntityType EntityType;
    public eEntityLookDir BaseDir;

}
public class CharacterBehaviourData : EntityBehaviourData
{
    public CharacterStat _characterStat;
}
