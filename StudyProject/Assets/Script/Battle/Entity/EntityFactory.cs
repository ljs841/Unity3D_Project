using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory
{
    private static EntityFactory _instance;
    public static EntityFactory _Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EntityFactory();
            }
            return _instance;
        }

    }

    public Entity  CreateEntityForBattle(string assetname ,eEntityType entityType , eEntityLookDir lookDir , int subType) 
    {
        GameObject behaviour = UnitObjectPool.Instance.GetCharacterGameObject(assetname);
        switch (entityType)
        {

            case eEntityType.InGameCharacter:
               
                Character entity = behaviour.gameObject.AddComponent<Character>();
                entity.Rigidbody = behaviour.gameObject.GetComponent<Rigidbody2D>();
                entity.AniControl = behaviour.GetComponent<SpriteAnimationController>();
                entity.Collider = behaviour.GetComponent<BoxCollider2D>();
                entity.Node = behaviour.GetComponentInChildren<Character_Node>();
                entity.Init(entityType, lookDir, subType);
                if(subType >1 )
                {
                    entity.AIStateManager = behaviour.AddComponent<AIStateManager>();
                    entity.AIStateManager.Init(entity);
                }
                entity.AllyType = subType > 1 ? eUnitAllyType.Enemy : eUnitAllyType.Player;
                UnitManager.Instance.AddUnit(behaviour.GetInstanceID(), entity);
                return entity;
            case eEntityType.InGameProjectile:
                Projectile projectile = behaviour.gameObject.AddComponent<Projectile>();

                projectile.Rigidbody = behaviour.gameObject.GetComponent<Rigidbody2D>();
                projectile.AniControl = behaviour.GetComponent<SpriteAnimationController>();
                projectile.Node = behaviour.GetComponentInChildren<Character_Node>();
                projectile.Init(entityType, lookDir, subType);
                projectile.AllyType = eUnitAllyType.EnemyAlly;
                UnitManager.Instance.AddProjectile(projectile);
                return projectile; ;
        }


        return null;
    }
}