using System;
using System.Collections.Generic;
using UnityEngine;
public class SimplePhysics
{

    protected Rigidbody2D _rigidBody;
    protected Vector2 _targetVelocity;
    public Vector2 Velocity
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

    float minGroundNormalY = .65f;
    float gravityModifier = 1f;

    protected bool _isGround;
    public bool IsGround
    {
        get
        {
            return _isGround;
        }
    }
    protected Vector2 _velocity;
    public Vector2 CurVelocity
    {
        get
        {
            return _velocity;
        }
    }
    protected Vector2 _groundNormal;

    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    protected const float _minMoveDistance = 0.001f;
    protected const float _shellRadius = 0.01f;

    public SimplePhysics(Rigidbody2D rigidBody)
    {
        _rigidBody = rigidBody;
    }

    public void PhysicsUpdate()
    {
        _velocity += gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        _velocity.x = _targetVelocity.x;

        _isGround = false;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);

        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    public void SetJumpForce(float value)
    {
        _velocity.y = value;
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidBody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);
            _hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    _isGround = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        _rigidBody.position = _rigidBody.position + move.normalized * distance;
    }
}