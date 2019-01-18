using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewLoopGrid : MonoBehaviour
{
    public GameObject _prefabSource;
    public RectTransform _content;
    public Vector2 _scrollItemSize;
    public Vector2 _itemPosOffset;
    public eScroolItemCreationType _creationType;
        
    public int _maxLineInObjectCount;
    private int _maxLineCount;
    List<ScrollViewDataModel> _itemDataList;

    int _visibleLineCount;
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
        _visibleLineCount = GetVisibleLineCount(viewportSize);
        _maxLineCount = CalContentMaxLine(_itemDataList.Count, _maxLineInObjectCount);
        _maxVisibleItemCount = _maxLineInObjectCount * (_visibleLineCount + 1);
               
        ScrollViewItemPoolManager._Instance.CreatePool(_maxVisibleItemCount , _prefabSource);
        _prvStartIndex = 0;
        _prvEndIndex = _maxVisibleItemCount - 1;
        
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

    public void RePosition(bool isFirstCall = false)
    {
        if (Application.isPlaying == false)
            return;

        int startDataIdx = TopLineFirstRowIndex();
        int endIndex = startDataIdx + _maxVisibleItemCount >= _itemDataList.Count ? _itemDataList.Count - 1 : startDataIdx + _maxVisibleItemCount - 1;
        ScrollViewItemPoolManager._Instance.CheckNoUseItem(startDataIdx, endIndex);

        int rowIdx = 0;
        int columnIdx = TopLineColumnIndex();

        for (int i = 0; i < _maxVisibleItemCount; i++)
        {
            if (startDataIdx >= _itemDataList.Count)
                break;
            if (isFirstCall)
            {
                ItemSetting(startDataIdx, rowIdx, columnIdx);
            }
            else if ((startDataIdx >= _prvStartIndex && startDataIdx <= _prvEndIndex) == false)
            {
                ItemSetting(startDataIdx, rowIdx, columnIdx);
            }

            startDataIdx++;
            rowIdx++;
            if (rowIdx >= _maxLineInObjectCount)
            {
                rowIdx = 0;
                columnIdx++;
            }
        }

        startDataIdx = TopLineFirstRowIndex();
        _prvStartIndex = startDataIdx;
        _prvEndIndex = endIndex;
    }

    public Vector2 GetConterntSizeToType()
    {
        Vector2 resultSize = new Vector2();
        var width = IsWidthLimit() ? _maxLineInObjectCount : _maxLineCount;
        var height = IsWidthLimit() ? _maxLineCount : _maxLineInObjectCount;
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
        int centerLine = (int)((_visibleLineCount -1) * 0.5f);
        scrollPos = scrollPos - centerLine <= 0 ?  0 :scrollPos - centerLine;
        Vector2 itemSize = AdditemOffSet(_scrollItemSize);
        float value = horizontal ? itemSize.x : itemSize.y;
        return (float)(scrollPos  * value);
    }

    int GetVisibleLineCount(Vector2 viewportSize)
    {
        Vector2 sizeAddOffset = AdditemOffSet(_scrollItemSize);
        float scrollTypeToViewportSize = horizontal ? viewportSize.x : viewportSize.y;
        float scrollTypeToItemtSize = horizontal ? sizeAddOffset.x : sizeAddOffset.y;
        return Mathf.CeilToInt(scrollTypeToViewportSize / scrollTypeToItemtSize);
    }

    Vector2 AdditemOffSet( Vector2 size)
    {
        Vector2 newValue = Vector2.zero;
        newValue.x = size.x + _itemPosOffset.x;
        newValue.y = size.y + _itemPosOffset.y;
        return newValue;
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
        Vector2 changeAxisVec = Vector2.zero;
        Vector2 pos = Vector2.zero;
        Vector2 originSize = horizontal ? ChangeValue(_scrollItemSize) : _scrollItemSize;
        Vector2 offsetSize = horizontal ? ChangeValue(_itemPosOffset) : _itemPosOffset;

        
        pos.x = (rowIdx * originSize.x) + ((rowIdx + 1) * offsetSize.x);
        pos.y = ((columnIdx * originSize.y) + ((columnIdx + 1) * offsetSize.y));

        changeAxisVec = IsWidthLimit() ? pos : ChangeValue(pos);
        changeAxisVec.y = -changeAxisVec.y;

        Util.AttachGameObject(_content.gameObject, obj.gameObject, false, false);
        obj.transform.localPosition = changeAxisVec;
        obj.Show();
        obj.UpdateData(_itemDataList[index]);
        obj.ViewUpdate();
        obj.name = index.ToString();
    }

    bool IsWidthLimit()
    {
        return _creationType == eScroolItemCreationType.WidthLimit;
    }
}