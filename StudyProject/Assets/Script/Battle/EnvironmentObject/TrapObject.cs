using System;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class TrapObject : MonoBehaviour
{
    public float _damegae;
    public Rigidbody2D _rigidBody;


    int _unitLayerMask = (1 << 8) | (1 << 9);
    RaycastHit2D[] _hitBuffer = new RaycastHit2D[20];
    List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>();
    private void FixedUpdate()
    {
        TapCheck();
    }

    void TapCheck()
    { //cast가 레이를 쏘는게 아니라 가지고있는 콜리더의 범위만큼 방향을 향해 거리체크를 하는듯 하다.
        int count = _rigidBody.Cast(Vector2.up, _hitBuffer, ConstValue._checkxGroundDistance);
        HitColliderInfoClear(count);
        for (int i = 0; i < _hitBufferList.Count; i++)
        {
            int layer = 1 << _hitBufferList[i].collider.gameObject.layer;
            if ((_unitLayerMask & layer) > 0)
            {
                UnitManager.Instance.SendTrapDameage(_hitBufferList[i].collider.gameObject.GetInstanceID() ,_damegae);
                
            }
        }
    }

    void HitColliderInfoClear(int count)
    {
        _hitBufferList.Clear();
        for (int i = 0; i < count; i++)
        {
            _hitBufferList.Add(_hitBuffer[i]);
        }
    }
}
