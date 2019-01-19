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
    public Transform _content;
    public Vector2 _scrollItemSize;
    public Vector2 _itemPosOffset;
        
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
               
        ScrollViewItemPoolManager._Instance.CreatePool(_maxVisibleItemCount , _prefabSource , this);
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

    public void RefreshItems()
    {
        int startDataIdx = TopLineFirstRowIndex();
        ScrollViewItemPoolManager._Instance.RetunAllItem();


        int rowIdx = 0;
        int columnIdx = TopLineColumnIndex();

        for (int i = 0; i < _maxVisibleItemCount; i++)
        {
            if (startDataIdx >= _itemDataList.Count)
                break;

            ItemSetting(startDataIdx, rowIdx, columnIdx);

            startDataIdx++;
            rowIdx++;
            if (rowIdx >= _maxLineInObjectCount)
            {
                rowIdx = 0;
                columnIdx++;
            }
        }

        _prvEndIndex = startDataIdx - 1;
        startDataIdx = TopLineFirstRowIndex();
        _prvStartIndex = startDataIdx;
    }

    public void RePosition(bool isFirstCall = false)
    {
        if (Application.isPlaying == false)
            return;

        int startDataIdx = TopLineFirstRowIndex();
        CheckNoUseItem(startDataIdx);

        int rowIdx = 0;
        int columnIdx = TopLineColumnIndex();

        for (int i = 0; i < _maxVisibleItemCount; i++)
        {
            if (startDataIdx >= _itemDataList.Count)
                break;
            
            if ((startDataIdx >= _prvStartIndex && startDataIdx <= _prvEndIndex) == false)
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
        _prvEndIndex = startDataIdx - 1;
        startDataIdx = TopLineFirstRowIndex();
        _prvStartIndex = startDataIdx;
        
    }

    public Vector2 GetConterntSizeToType()
    {
        Vector2 resultSize = new Vector2();
        var width = IsStarAxisHorizontal() ? _maxLineInObjectCount : _maxLineCount;
        var height = IsStarAxisHorizontal() ? _maxLineCount : _maxLineInObjectCount;
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

    public void DeleteData(int index)
    {
        _itemDataList.RemoveAt(index);
        _maxLineCount = CalContentMaxLine(_itemDataList.Count, _maxLineInObjectCount);
        _maxVisibleItemCount = _maxLineInObjectCount * (_visibleLineCount + 1);
        RefreshItems();
    }

    void CheckNoUseItem(int startDataIdx)
    {
        int endIDataIdx = startDataIdx + _maxVisibleItemCount >= _itemDataList.Count ? _itemDataList.Count - 1 : startDataIdx + _maxVisibleItemCount - 1;

        ScrollViewItemPoolManager._Instance.CheckNoUseItem(startDataIdx, endIDataIdx);
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

        changeAxisVec = IsStarAxisHorizontal() ? pos : ChangeValue(pos);
        changeAxisVec.y = -changeAxisVec.y;

        Util.AttachGameObject(_content.gameObject, obj.gameObject, false, false);
        obj.transform.localPosition = changeAxisVec;
        obj.Show();
        obj.UpdateData(_itemDataList[index]);
        obj.ViewUpdate();
        obj.name = index.ToString();
    }

    bool IsStarAxisHorizontal()
    {
        return _startAxis == Axis.Horizontal;
    }
}