using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    Vector3 CalResultPosition(Vector3 curForwardVec, Vector3 curPositionVec, float speed);
}
