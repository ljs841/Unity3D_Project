using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static bool _isShowConsoleLog = true;

    public static void AttachGameObject(GameObject parent , GameObject prefabObj , bool worldPosStays , bool positionReset)
    {
        prefabObj.transform.SetParent(parent.transform, worldPosStays);
        if(positionReset)
        {
            prefabObj.transform.position = positionReset ? Vector3.zero : prefabObj.transform.position;
        }

    }

    public static void DebugLog(string msg)
    {
        if (_isShowConsoleLog == false)
            return;
        Debug.Log(msg);
    }

    public static void DebugErrorLog(string msg)
    {
        if (_isShowConsoleLog == false)
            return;
        Debug.LogError(msg);
    }
    
    public static bool RectContains(RectTransform  a, RectTransform b)
    {
        /*GetWorldCorners 순서
         * 0 – Bottom Left
         * 1 – Top Left
         * 2 – Top Right
         * 3 – Bottom Right
         */
        Vector3[] aWorldCorners = new Vector3[4];
        Vector3[] bWorldCorners = new Vector3[4];
        a.GetWorldCorners(aWorldCorners);
        b.GetWorldCorners(bWorldCorners);

        //if(aWorldCorners<=)

        Rect worldRect = new Rect(a.position, a.rect.size);
        bool isContains = true;
        foreach(var pos in bWorldCorners)
        {
            if(worldRect.Contains(pos) == false)
            {
                isContains = false;
            }
        }

        return isContains;
    }

    public static bool Intersects(Rect rectA, Rect rectB)
    {
        return !((rectB.x + rectB.width <= rectA.x) ||
                (rectB.y + rectB.height <= rectA.y) ||
                (rectB.x >= rectA.x + rectA.width) ||
                (rectB.y >= rectA.y + rectA.height));
    }

    public static bool Intersects(Rect rectA, Vector2 pos , Vector2 size)
    {
        return !((pos.x + size.x <= rectA.x) ||
                (pos.y + size.y <= rectA.y) ||
                (pos.x >= rectA.x + rectA.width) ||
                (pos.y >= rectA.y + rectA.height) );
    }

    public static float LogicInterval(eEntityType type)
    {

        switch (type)
        {
            case eEntityType.InGameCharacter:
                return ConstValues.ConstValue._characterTypeLogicInterval;

            default:
                return ConstValues.ConstValue._defaultTypeLogicInterval;
        }

    }

    public static Vector3 CalResultPosition(Vector3 curForwardVec, float speed)
    {
        return curForwardVec * speed;
    }

    public static Vector3 Dir2DConvert3D(eEntityLookDir dir)
    {
        Vector3 forward = Vector3.zero;
        switch (dir)
        {
            case eEntityLookDir.Left:
                forward.x = -1;
                break;
            case eEntityLookDir.Right:
                forward.x = 1;
                break;
        }
        forward.Normalize();
        return forward;
    }


}

