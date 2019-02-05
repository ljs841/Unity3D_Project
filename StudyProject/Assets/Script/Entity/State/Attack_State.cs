using System;
using System.Collections.Generic;

public class Attack_State : CharacterState
{
    public Attack_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }
}