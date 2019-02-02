using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    protected CharacterStat _stat;
    public CharacterStat Stat
    {
        get
        {
            return _stat;
        }
    }

    public Character(eEntityType type, eEntityLookDir baseDir, int subType) : base(type , baseDir ,subType)
    {
        var stat = TempData.GetCharacterData(type, subType); 
        if(stat != null)
        {
            _stat = stat;
        }
    }

    public override void Init(EntityBehaviour mono)
    {
        base.Init(mono);

        SetBehaviourData();
        IsActive = true;
       
    }

    protected override EntityBehaviourData SetBehaviourData()
    {
        CharacterBehaviourData data = new CharacterBehaviourData();
        data.BaseDir = _baseLookDir;
        data.EntityType = _type;
        data._characterStat = _stat;
        return data;
    }

    public void PublishInputEvent(InputManager input)
    {
        input.InputEvent += OnInputEvent;
    }


    public void UnPublishInputEvent(InputManager input)
    {

    }

    void OnInputEvent(object sender, BattleInputEventArgs e)
    {
        eEntityLookDir lookDir = e.InputType == eInputType.LeftPress ? eEntityLookDir.Left : eEntityLookDir.Right;
        _behaviour.SetLookDir(lookDir);
        _behaviour.GetButtonEvent(e.InputType);

    }
}
