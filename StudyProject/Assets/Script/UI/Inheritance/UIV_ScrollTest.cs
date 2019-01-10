using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIV_ScrollTest : UIComponent
{
    public LoopScroll _scroll;

    public void CreateScroll(List<ScrollViewDataModel> itemDataList)
    {
        _scroll.CreateScroll(itemDataList);
    }

}