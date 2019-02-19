using System;
using System.Collections.Generic;

public class Skill_State : CharacterState
{
    public Skill_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }
}