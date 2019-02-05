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
    Idle,
    Jump,
    Run,
    Attack,
    Die,
    Rise,

}
public enum eEntityType
{
    None,
    InGameCharacter,
    InGameTile,
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
    NonMove,
    LeftPress,
    LeftUp,
    RightPress,
    RightUp,
    Jump,
}

public enum eBundleLoadType
{
    None,
    Static,     // 패치씬에서 정적 타입은 그곳에서 로드를 해둔다.
    Dynamic,    // 동적은 필요한 씬에 준비 과정에서 로드를 하고 씬이 없어질 때 언로드 처리
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
