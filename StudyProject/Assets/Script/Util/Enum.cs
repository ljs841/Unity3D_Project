using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum eUILayer
{
    Layer1 = 0,
    Layer2,
    Layer3,
    Layer4
}

public enum eUIType
{
    uGUI,
    NGUI,
}

public enum eAnimationStateName
{
    None,
    Hide,
    Idle,
    Run,
    Attack,
    Die,
    Crouch,
    Crouch_Attack,
    Jump,
    Jump_Attack,
    Hurt,
    Dizzy,
    Rise,
}

public enum eAiState
{
    None,
    Idle,
    MoveToTarget,
    Attack,

}


public enum eEntityType
{
    None,
    InGameCharacter,
    InGameProjectile,
    UICharacter,
}

public enum eEntityLookDir
{
    None,
    Left,
    Right,
}

public enum eCharacterType
{
    None,
    Hero,
    Enemy,
}

public enum eInputType
{
    None,
    NonMove,
    LeftPress,
    LeftUp,
    RightPress,
    RightUp,
    Move,
    Jump,
    Crouch,
    Attak,
    Skill,
}

public enum eUnitAllyType
{
    None,
    Player,
    PlayerAlly,
    Enemy,
    EnemyAlly,

}




public enum eAttackType
{
    None,
    Melee,
    Range,
}

public enum eDameageType
{
    None,
    Slash,
    Punch,
    Trap,
    Magic,
}

public enum eCondition
{
    None,
    Immune,
    D_O_T,
    H_O_T,
}

public enum eFxType
{
    None,
    Immune,
    DIe,
    Hit1,
    EnemyDie,
}


public enum eBundleLoadType
{
    None,
    Static,     // 패치씬에서 정적 타입은 그곳에서 로드를 해둔다.
    Dynamic,    // 동적은 필요한 씬에 준비 과정에서 로드를 하고 씬이 없어질 때 언로드 처리
}

public enum eAssetTypeInGame
{
    None,
    Map,
    Unit,
    Fx,
    SFX,
}


//아이템 생성 방식
//HorizontalStart 일경우 width값을 구할 수 있기 때문에 가로로 생성을 시작해 점점 밑으로 생성하는 방식
//VerticalStart 일경우 height값을 구할 수 있기 때문에 세로로 생성을 시작해 점점 옆으로 생성하는 방식
public enum eScroolItemCreationType
{
    //VerticalInfinity  = 0 ,
    WidthLimit,
    //HorizontalInfinity,
    HeightLimit,
}

public enum eSoundType
{
    None,
    Background,
    Slash,
    Fireball,
    Walk,
    Hit,
    Punch,
}
