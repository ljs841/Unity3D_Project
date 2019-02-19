using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTable
{
    //    Character enemy1 = (Character)EntityFactory._Instance.CreateEntityForBattle("ghost", eEntityType.InGameCharacter, eEntityLookDir.Right, 4);

    public string _name;
    public int _tabelNumber;
    public eEntityLookDir _baseLook;

    public CharacterTable(string name , int tableNumber , eEntityLookDir baseLookDir)
    {
        _name = name;
        _tabelNumber = tableNumber;
        _baseLook = baseLookDir;
    }
}