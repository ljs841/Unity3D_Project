using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridInItemManager : MonoBehaviour
{
    public enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }
    public enum Corner
    {
        UpperLeft = 0,
        UpperRight = 1,
        LowerLeft = 2,
        LowerRight = 3
    }

    public Axis _startAxis;
    public Corner _startCorner;
    public GameObject _prefabSource;
    public RectTransform _content;
    public Vector2 _scrollItemSize;
    public Vector2 _itemPosOffset;
        
    public int _maxLineInObjectCount;

    List<ScrollViewDataModel> _itemDataList;

    int _maxVisibleLineCount;
    int _maxVisibleItemCount;

    int _prvStartIndex = 0;
    int _prvEndIndex = 0;

    float _contentTopToVisibleTopSize;
    bool vertical;
    bool horizontal;

    public void InitGrid (Vector2 viewportSize, List<ScrollViewDataModel> dataList , bool enableHorizontal , bool enableVertical) 
    {
        horizontal = enableHorizontal;
        vertical = enableVertical;
        _itemDataList = dataList;

        _maxVisibleLineCount = GetVisibleLineCount(viewportSize);
    
        CalGridElement();
        SetContentViewSize();

        SetContentAnchor();
        ScrollViewItemPoolManager._Instance.CreatePool(_maxVisibleItemCount , _prefabSource , this);

        _prvStartIndex = 0;
        _prvEndIndex = _maxVisibleItemCount - 1;
     
    }

    public void SetContentViewSize()
    {
        var sizeVector = GetConterntSizeToType();
        _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeVector.x);
        _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeVector.y);
    }

    public bool IsChangeContentView()
    {
        var startIndex = TopLineFirstRowIndex();      
        return _prvStartIndex != startIndex;
    }

    public void SetTopToViewCenterSize(float toptoCenterSize)
    {
        _contentTopToVisibleTopSize = toptoCenterSize;
    }

    public void RefreshItems(bool isCheckVisibleIdx = false)
    {
        int startDataIdx = TopLineFirstRowIndex();

        if (isCheckVisibleIdx)
        {
            CheckNoUseItem(startDataIdx);
        }
        else
        {
            ScrollViewItemPoolManager._Instance.ReturnAllItem();
        }

        for (int columnIdx = TopLineColumnIndex(); columnIdx <= TopLineColumnIndex() + _maxVisibleLineCount; columnIdx++)
        {
            for (int rowIdx = 0; rowIdx < _maxLineInObjectCount; rowIdx++)
            {
                if (startDataIdx >= _itemDataList.Count)
                    break;
                if (isCheckVisibleIdx && IsVisibleItem(startDataIdx) )
                {
                    startDataIdx++;
                    continue;
                }
                ItemSetting(startDataIdx, rowIdx, columnIdx);
                startDataIdx++;
            }

        }
        _prvEndIndex = startDataIdx - 1;
        startDataIdx = TopLineFirstRowIndex();
        _prvStartIndex = startDataIdx;
    }

    public Vector2 GetConterntSizeToType()
    {
        int maxLineCOunt = CalContentMaxLine(_itemDataList.Count, _maxLineInObjectCount);

        Vector2 resultSize = new Vector2();
        var width = IsStarAxisHorizontal() ? _maxLineInObjectCount : maxLineCOunt;
        var height = IsStarAxisHorizontal() ? maxLineCOunt : _maxLineInObjectCount;
        resultSize.x = width * (_scrollItemSize.x + _itemPosOffset.x) + _itemPosOffset.x;
        resultSize.y = height * (_scrollItemSize.y + _itemPosOffset.y) + _itemPosOffset.x;
        return resultSize;
    }

    public int CalContentMaxLine(int itemCount, int maxLineInObjectCount)
    {
        var visibleLine = itemCount / maxLineInObjectCount;
        visibleLine += itemCount % maxLineInObjectCount != 0 ? 1 : 0;
        return visibleLine;
    }

    public float GetIndexToScrollPosotion(int dataIndex)
    {
        //scrollPos 에 이미 쎈터라인의 포지션값이 포함되어있기때문에 라인값에서 1을 빼준다.
        int scrollPos = (int)(dataIndex / _maxLineInObjectCount);
        int centerLine = (int)((_maxVisibleLineCount -1) * 0.5f);
        scrollPos = scrollPos - centerLine <= 0 ?  0 :scrollPos - centerLine;
        Vector2 itemSize = SizeAddOffset();
        float value = horizontal ? itemSize.x : itemSize.y;
        return (float)(scrollPos  * value);
    }

    public void DeleteData(int index)
    {
        _itemDataList.RemoveAt(index);
        CalGridElement();
        SetContentViewSize();
        RefreshItems();
    }

    /// <summary>
    /// 스크롤의 위치가 LeftTop의 끝인지 체크한다.
    /// </summary>
    /// <returns></returns>
    public bool IsTopOver(Vector2 pos)
    {
        if (horizontal)
        {
            if (_startCorner == Corner.LowerLeft || _startCorner == Corner.UpperLeft)
            {
                return _content.anchoredPosition.x >= pos.x;
            }
            else
            {
                return _content.anchoredPosition.x <= pos.x;
            }
           
        }
        else
        {
            if (_startCorner == Corner.UpperLeft || _startCorner == Corner.UpperRight)
            {
                return _content.anchoredPosition.y <= pos.y;
            }
            else
            {
                return _content.anchoredPosition.y >= pos.y;
            }
        }

        
    }

    bool IsVisibleItem(int idx)
    {
        return idx >= _prvStartIndex && idx <= _prvEndIndex;
    }

    void SetContentAnchor()
    {
        Vector2 value = Vector2.zero;
        switch (_startCorner)
        {
            case Corner.UpperLeft:
                value.x = 0;
                value.y = 1;               
                break;
            case Corner.UpperRight:
                value.x = 1;
                value.y = 1;
                break;
            case Corner.LowerLeft:
                value.x = 0;
                value.y = 0;
                break;
            case Corner.LowerRight:
                value.x = 1;
                value.y = 0;
                break;
        }
        _content.anchorMin = value;
        _content.anchorMax = value;
        _content.pivot = value;
    }

    void CalGridElement()
    {
        _maxVisibleItemCount = _maxLineInObjectCount * (_maxVisibleLineCount + 1);
    }

    void CheckNoUseItem(int startDataIdx)
    {
        int endIDataIdx = startDataIdx + _maxVisibleItemCount >= _itemDataList.Count ? _itemDataList.Count - 1 : startDataIdx + _maxVisibleItemCount - 1;
        ScrollViewItemPoolManager._Instance.CheckNoUseItem(startDataIdx, endIDataIdx);
    }

    int GetVisibleLineCount(Vector2 viewportSize)
    {
        Vector2 sizeAddOffset = SizeAddOffset();
        float scrollTypeToViewportSize = horizontal ? viewportSize.x : viewportSize.y;
        float scrollTypeToItemtSize = horizontal ? sizeAddOffset.x : sizeAddOffset.y;
        return Mathf.CeilToInt(scrollTypeToViewportSize / scrollTypeToItemtSize);
    }

    Vector2 SizeAddOffset()
    {
        return _scrollItemSize + _itemPosOffset;
    }

    int GetCurrentTopLine()
    {
        return Mathf.CeilToInt(_contentTopToVisibleTopSize / ScrollTypeToValue(_scrollItemSize, true));
    }
    
    float ScrollTypeToValue(Vector2 vec, bool addOffset = false, bool addPadding = false)
    {
        float value = horizontal ? vec.x : vec.y;
        if (addOffset)
        {
            value += horizontal ? _itemPosOffset.x : _itemPosOffset.y;
        }
        if (addPadding)
        {
            //not yet
        }
        return value;
    }

    /// <summary>
    /// 현재 뷰포트 중앙에 위치한 라인값으로 배열의 인덱스를 계산
    /// 중앙 라인 에서 현재 보이는라인 갯수의 반만큼 빼주면 첫번째 라인이 된다.
    /// 배열 인덱스는 0부터 시작하니 -1 추가 계산
    /// 최대 라인에 표시될 오브젝트 갯수를 곱해주면 최종값
    /// </summary>
    /// <returns></returns>
    int TopLineFirstRowIndex()
    {
        return TopLineColumnIndex() * _maxLineInObjectCount; ;
    }

    int TopLineColumnIndex()
    {
        var index = GetCurrentTopLine();
        index = index  <= 1 ? 0 : index - 1;
        return index;
    }
    
    Vector2 ChangeValue(Vector2 vec)
    {
        float value = vec.x;
        vec.x = vec.y;
        vec.y = value;
        return vec;
    }

    void ItemSetting(int index, int rowIdx, int columnIdx)
    {
        var obj = ScrollViewItemPoolManager._Instance.GetItem(index);
        Vector2 pos = Vector2.zero;
        int calRow = IsStarAxisHorizontal() ? rowIdx : columnIdx;
        int calcolumn = IsStarAxisHorizontal() ? columnIdx : rowIdx;

        pos.x = (calRow * _scrollItemSize.x) + ((calRow + 1) * _itemPosOffset.x);
        pos.y = ((calcolumn * _scrollItemSize.y) + ((calcolumn + 1) * _itemPosOffset.y));
        pos = CalConerToPos(pos);

        Util.AttachGameObject(_content.gameObject, obj.gameObject, false, false);
        obj.transform.localPosition = pos;

        obj.Show();
        obj.UpdateData(_itemDataList[index]);
        obj.ViewUpdate();
        obj.name = index.ToString();
    }

    Vector2 CalConerToPos(Vector2 pos)
    {
        switch (_startCorner)
        {
            case Corner.UpperLeft:
                pos.y = -pos.y;
                break;
            case Corner.UpperRight:
                pos.x = -(pos.x + _scrollItemSize.x);
                pos.y = -(pos.y ) ;
                break;
            case Corner.LowerLeft:
                pos.y += _scrollItemSize.y;
                break;
            case Corner.LowerRight:
                pos.x = -(pos.x + _scrollItemSize.x);
                pos.y += _scrollItemSize.y;
                break;
        }

        return pos;

    }

    bool IsStarAxisHorizontal()
    {
        return _startAxis == Axis.Horizontal;
    }
}