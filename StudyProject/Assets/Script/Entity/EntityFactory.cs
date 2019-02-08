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

    public Entity  CreateEntityForBattle(eEntityType entityType , eEntityLookDir lookDir , int subType) 
    {
       
        switch (entityType)
        {

            case eEntityType.InGameCharacter:
                GameObject behaviour = UnitObjectPool._Instance.GetCharacterGameObject();
                Character entity = behaviour.gameObject.AddComponent<Character>();
                entity.Rigidbody = behaviour.gameObject.GetComponent<Rigidbody2D>();
                entity.AniControl = behaviour.GetComponent<SpriteAnimationController>();
                entity.Init(entityType, lookDir, subType);
                return entity;
        }


        return null;
    }
}