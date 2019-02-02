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
                Character entity = new Character(entityType, lookDir , subType);
                CharacterBehaviour behaviour = UnitObjectPool._Instance.GetCharacterGameObject();
                entity.Init(behaviour);
                return entity;
        }


        return null;
    }
}