using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoopScroll : ScrollRect
{
    public ScrollViewLoopGrid _ItemGrid;
    Rect _contentVisibleRect;

    public void CreateScroll(List<ScrollViewDataModel> dataList)
    {
        //스크롤 뷰는 화면을 표시할 뷰포트와 컨텐츠영역을 가지고있다
        //뷰포트의 사이즈는 컨텐츠 영역에서 보일수있는 컨텐츠수를 추측할수 있다.
        _ItemGrid.InitGrid(viewport.rect.size , dataList , horizontal , vertical);
        Vector2 calSize = _ItemGrid.GetConterntSizeToType();
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, calSize.x);
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, calSize.y);
         _contentVisibleRect = new Rect(content.anchoredPosition, viewport.rect.size);
        _ItemGrid.RePosition(true);
    }

    protected override void LateUpdate()
    {
        if (Application.isPlaying == false)
            return;
        base.LateUpdate();
        UpdateContentRect();
        _ItemGrid.SetTopToViewCenterSize(CalCenterSize());
        if(IsTopOver() == false &&_ItemGrid.IsChangeContentView())
        {
            _ItemGrid.RePosition();
        }
    }

    bool IsTopOver()
    {
        if(horizontal)
        {
            return content.anchoredPosition.x >= _contentVisibleRect.x;
        }
        return content.anchoredPosition.y <= _contentVisibleRect.y;


    }

    void UpdateContentRect()
    {
        if(_contentVisibleRect.size == Vector2.zero)
        {
            _contentVisibleRect.size = viewport.rect.size;
        }
        _contentVisibleRect.position = GetCurViewportToContentLocal();
    }

    Vector2 GetCurViewportToContentLocal()
    {
        Vector2 pos = Vector2.zero;
        if (horizontal)
        {
            pos.x = -content.anchoredPosition.x;
        }
        if (vertical)
        {
            pos.y = -content.anchoredPosition.y;
        }
        return pos;
    }

    float CalCenterSize()
    {
        //return  Vector2.Distance(Vector2.zero, _contentVisibleRect.position) + ScrollTypeToValue(_contentVisibleRect.size) * 0.5f;
        return Vector2.Distance(Vector2.zero, _contentVisibleRect.position);
    }

    float ScrollTypeToValue(Vector2 vec, bool addOffset = false, bool addPadding = false)
    {
        float value = horizontal ? vec.x : vec.y;
        if (addOffset)
        {
            value += horizontal ? _ItemGrid._itemPosOffset.x : _ItemGrid._itemPosOffset.y;
        }
        if (addPadding)
        {
            //not yet
        }
        return value;
    }
}
