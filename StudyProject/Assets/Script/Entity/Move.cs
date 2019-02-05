using System;
using System.Collections.Generic;
using UnityEngine;

public class Move : IMove
{
    protected Vector3 _forwardVec;
    protected Vector3 _positionVec;

    public virtual void SetForward(Vector3 curForwardVec)
    {
        _forwardVec = curForwardVec;
    }

    public virtual void SetPosition(Vector3 curPositionVec)
    {
        _positionVec = curPositionVec;
    }

    public virtual Vector3 CalResultPosition(Vector3 curForwardVec , Vector3 curPositionVec , float speed)
    {
        return Vector3.zero;
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
