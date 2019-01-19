using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoopScroll : ScrollRect
{

    public GridInItemManager _ItemGrid;
    Rect _contentVisibleRect;

    public void CreateScroll(List<ScrollViewDataModel> dataList)
    {
        //스크롤 뷰는 화면을 표시할 뷰포트와 컨텐츠영역을 가지고있다
        //뷰포트의 사이즈는 컨텐츠 영역에서 보일수있는 컨텐츠수를 추측할수 있다.
        _ItemGrid.InitGrid(viewport.rect.size , dataList , horizontal , vertical);
        SetContentViewSize(_ItemGrid.GetConterntSizeToType());
         _contentVisibleRect = new Rect(content.anchoredPosition, viewport.rect.size);
        _ItemGrid.RefreshItems();
    }

    public void JumpToIndex(int dataIndex)
    {
        float pos = _ItemGrid.GetIndexToScrollPosotion(dataIndex);

        Vector2 vec = Vector2.zero;
        vec.x = horizontal ? pos : 0;
        vec.y = horizontal ? 0 : pos;
        SetContentAnchoredPosition(vec);
    }

    protected override void LateUpdate()
    {
        if (Application.isPlaying == false)
            return;
        base.LateUpdate();
        UpdateContentRect();
        _ItemGrid.SetTopToViewCenterSize(CalContentViewTopToVisibleRectTopDistance());

        if (IsTopOver() && _ItemGrid.IsChangeContentView())
        {
            _ItemGrid.RePosition();
        }
        else if (_ItemGrid.IsChangeContentView())
        {
            _ItemGrid.RePosition();
        }
    }

    void SetContentViewSize(Vector2 size)
    {
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }
    
    /// <summary>
    /// 스크롤의 위치가 LeftTop의 끝인지 체크한다.
    /// </summary>
    /// <returns></returns>
    bool IsTopOver()
    {
        if (horizontal)
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

    /// <summary>
    /// 컨텐츠뷰 앵커를 TppLeft로 맞췄을 경우 
    /// 현재 컨텐츠뷰가 이동한 거리만큼의 음수값을 넣어주면 현재 보이는 Rect값을 구할수있다.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 컨텐츠 뷰의 LeftTop응 앵커 포지션으로잡을때 LateUpdate에서 매번 갱신되는 현재 보이고있는 Rect로
    /// 거리값을 구한다. 그러면 스크롤인덱스값을 추출할수 있다.
    /// </summary>
    /// <returns></returns>
    float CalContentViewTopToVisibleRectTopDistance()
    {
        return Vector2.Distance(Vector2.zero, _contentVisibleRect.position);
    }
}
