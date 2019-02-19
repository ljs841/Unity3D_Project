using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    
    eDameageType _dameageType;
    float _dameageValue;
    float _speed;

    public override void Init(eEntityType type, eEntityLookDir baseDir, int subType)
    {
        base.Init(type, baseDir, subType);
        _physics = new SimplePhysics(_rigidbody, _node );
        _physics.AttackColliderList += OnHitColliderList;
        _physics.SomethingCollide += OnSomethingCollide;
        _physics.SetGrivityModify(0);
        _speed = 5;
        _dameageType = eDameageType.Magic;
        _dameageValue = 50;

        SetForward(Util.Dir2DConvert3D(baseDir));

    }
    public void FireProjectile(Vector2 dir)
    {
        _aniControl.SetSpriteFlip(_baseLookDir, dir);
        _aniControl.PlayAnimation(eAnimationStateName.Idle);
        _physics.TragetVelocity = dir * _speed;
        _physics.SetAttack(true);
    }
    void OnSomethingCollide(bool isCollide)
    {
        if(isCollide)
        {
            DeActiveBehavior(); 
        }
    }
    void OnHitColliderList(List<RaycastHit2D> list)
    {
        bool isCollide = false;
        foreach (var hitObj in list)
        {
            Character obj = UnitManager.Instance.GetChar(hitObj.collider.gameObject.GetInstanceID());
            if (obj != null &&   Util.CheckAlly(_allyType,  obj.AllyType ) == false)
            {
                obj.OnDameage(null, obj, Forward, _dameageValue);
                isCollide = true;
            }
        }
        if(isCollide)
        {
            DeActiveBehavior();
        }


    }
    private void FixedUpdate()
    {
        FixedUpdateEntity();
    }

    public override void FixedUpdateEntity()
    {
        base.FixedUpdateEntity();
        _physics.PhysicsUpdate();
    }

}