using System;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;
public class SimplePhysics
{

    protected Rigidbody2D _rigidBody;
    protected Vector2 _targetVelocity;
    public Vector2 TragetVelocity
    {
        get
        {
            return _targetVelocity;
        }
        set
        {
            _targetVelocity = value;
        }
    }
    
    protected bool _isGround;
    public bool IsGround
    {
        get
        {
            return _isGround;
        }
    }
    protected bool _isJumpState;
    public bool isJumpState
    {
        get
        {
            return _isJumpState;
        }
    }
    public Vector2 Forward
    {
        get
        {
            return _forward;
        }
    }
    
    public Action <List<RaycastHit2D>> AttackColliderList;
    public Action<bool> SomethingCollide;
    #region 전역 변수


    Character_Node _node;

    //ground Check
    Vector2 _groundNormal;
    float _gravityModify;
    bool _groundNear = false;

    //forward check
    Vector2 _forward = Vector2.zero;
    int _wallLayerMask = (1 << 10) | (1 << 11) | (1 << 12);

  
    //jump
    float _jumpDeltaTime = 0;
    float _targetHeight = 0;
    bool _jumpHeightMax = false;

    //attack
    bool _isAttackState = false;
    int _unitLayerMask = (1 << 8) | (1 << 9);
    float _attackOffset =0.0f;

    //리지드 바디가 실제로 Time.fixeddeltaTime마다 이동하는 속도
    Vector2 _velocity;

    //collider 체크
    RaycastHit2D[] _hitBuffer = new RaycastHit2D[20];
    RaycastHit2D[] _attackHitBuffer = new RaycastHit2D[20];
    List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>();

    #endregion

    public SimplePhysics(Rigidbody2D rigidBody , Character_Node node)
    {
        _rigidBody = rigidBody;
        _node = node;
        _gravityModify = 1.0f;
   
       
    }
    public void SetGrivityModify(float value)
    {
        _gravityModify = value;
    }

    public void PhysicsUpdate()
    {
        SetVelocityInfo();

        float distance = _velocity.magnitude;
        if (distance > ConstValue._minMoveDistance)
        {
            RigidBodyCast(Vector2.down, ConstValue._groundCastRayDistance);
            GroundCheck();
            WallCheck();
        }
        _rigidBody.position = _rigidBody.position + _velocity;

        AttackCast();
    }

    public void SetAttack(bool isState)
    {
        _isAttackState = isState;
    }

    public void SetForward(Vector2 vec)
    {
        if (vec.x == 0)
            return;
        _forward = vec;
    }

    void OnSomethingCollide(bool isCollide)
    {
        if(SomethingCollide != null)
        {
            SomethingCollide(isCollide);
        }
    }

    float CallAttackOffset()
    {
        Vector3 pos = _node.MeleeAttackPos.transform.localPosition;
        pos.x *= -1;
        return Vector3.Distance(_node.MeleeAttackPos.transform.localPosition, pos);
    }

    void AttackCast()
    {
        if(_isAttackState)
        {
            Vector3 pos = _node.MeleeAttackPos.transform.position;
            pos.x = _forward.x < 0 ? pos.x - CallAttackOffset() : pos.x;
            int count = Physics2D.BoxCastNonAlloc(pos, _node.MeleeCheckRectSize, 0, _forward, _attackHitBuffer, 0.3f, _unitLayerMask);
            _hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_attackHitBuffer[i]);
            }

            if (_hitBufferList.Count > 0 && AttackColliderList != null)
            {
                AttackColliderList(_hitBufferList);
            }
        }
    }


    public void SetJump(float value)
    {
        _jumpDeltaTime = 0;
        _targetHeight = 0;
        _isJumpState = true;
        _jumpHeightMax = false;
    }

  
    float Jump()
    {
        float prvHeight = _targetHeight;
        if (_jumpHeightMax == true)
            return 0.0f;
        var height = Mathf.Sin(Mathf.PI * _jumpDeltaTime * 2) * _targetVelocity.y;
        _jumpDeltaTime  += Time.fixedDeltaTime;
        _targetHeight = height;
        _jumpHeightMax = _targetHeight >= _targetVelocity.y - 0.01f ? true : false;
        return _targetHeight - prvHeight;
    }

    void WallCheck()
    {
        int count = Physics2D.BoxCastNonAlloc(_node.Middle.position, _node.ForwardCheckRectSize, 0, _forward, _hitBuffer, ConstValue._forwardCastRayDistance, _wallLayerMask);
        HitColliderInfoClear(count);
        bool isWallCollider = false;
        for (int i = 0; i < _hitBufferList.Count; i++)
        {
            int layer =1 << _hitBufferList[i].collider.gameObject.layer;
            if ((_wallLayerMask & layer) > 0)
            {
                isWallCollider = true;
                _velocity.x = 0;
            }
        }
        if(isWallCollider)
        {
            OnSomethingCollide(isWallCollider);
        }
    }

    void GroundCheck()
    {
        _isGround = false;
        _groundNear = false;
        for (int i = 0; i < _hitBufferList.Count; i++)
        {
           
            //충돌한 지형의 노말 벡터 얻고 지형 노말벡터로 설정
            Vector2 currentNormal = _hitBufferList[i].normal;
            if (currentNormal.y > ConstValue._minGroundNormalY)
            {
                //_wallLayerMask
                int layer = 1 << _hitBufferList[i].collider.gameObject.layer;
                if ((_wallLayerMask & layer) <= 0)
                    continue;
                

                    _groundNear = _hitBufferList[i].distance <= ConstValue._checkxGroundDistance ? true : false;
                if (_hitBufferList[i].distance <= ConstValue._minGroundDistance)
                {
                    _isGround = true;
                    _isJumpState = _jumpHeightMax ? false : true;
                    _groundNormal = currentNormal;
                }
            }
            if (_isGround)
            {
                _groundNear = false;               
                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }
            }
            else
            {
                SetMinVelocity();
            }
        }
    }
    
    void RigidBodyCast(Vector2 dir, float rayDistance)
    {
        //cast가 레이를 쏘는게 아니라 가지고있는 콜리더의 범위만큼 방향을 향해 거리체크를 하는듯 하다.
        int count = _rigidBody.Cast(dir, _hitBuffer, rayDistance);
        HitColliderInfoClear(count);
    }

    void HitColliderInfoClear(int count)
    {
        _hitBufferList.Clear();
        for (int i = 0; i < count; i++)
        {
            _hitBufferList.Add(_hitBuffer[i]);
        }
    }
    
    /// <summary>
    /// 중력이나 프레임의 문제로 케릭터가 크게 이동할경우 충돌체크가 되지않는 경우가있다.
    /// 그래서 지면에 거의 도달했으면 최소속도를 적용하여 지면에 도착한것 처럼 보이지만 조금씩 느려지며 지면에 가깝에 이동한다.
    /// </summary>
    void SetMinVelocity()
    {
        _velocity.y = _groundNear && _velocity.y <= ConstValue._minVelocity_Y ? ConstValue._minVelocity_Y : _velocity.y;
    }

    void SetVelocityInfo()
    {
        _velocity += _gravityModify * Physics2D.gravity * Time.fixedDeltaTime;
        _velocity.x = _targetVelocity.x * Time.fixedDeltaTime;
        _velocity.y = _jumpHeightMax == false ? Jump() : _velocity.y;
        if (_velocity.y <= ConstValue._limitGravity)
        {
            _velocity.y = ConstValue._limitGravity;
        }
    }

}