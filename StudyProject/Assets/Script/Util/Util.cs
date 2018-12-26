using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void AttachGameObject(GameObject parent , GameObject prefabObj , bool positionReset = true)
    {
        prefabObj.transform.parent = parent.transform;
        prefabObj.transform.position = positionReset? Vector3.zero : prefabObj.transform.position;
    }

}

