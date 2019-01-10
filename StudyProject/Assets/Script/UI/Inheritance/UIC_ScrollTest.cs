using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIC_ScrollTest : UIContentController
{
   
    public override void Create()
    {
        base.Create();

        List<ScrollViewDataModel> _itemDataList = new List<ScrollViewDataModel>();
        for (int i = 0; i < 3000; i++)
        {
            _itemDataList.Add(new ScrollViewDataModel());
        }


        var comopnent = (UIV_ScrollTest)_component;
        comopnent.CreateScroll(_itemDataList);

    }
}
