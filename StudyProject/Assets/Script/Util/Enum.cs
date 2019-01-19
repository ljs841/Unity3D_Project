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
