using System;
using System.Collections.Generic;
using UnityEngine;
public static class TempData
{
    public static CharacterStat GetCharacterData(eEntityType type, int subType)
    {

        if (type == eEntityType.InGameCharacter)
        {
            CharacterStat _stat = new CharacterStat("power", subType);
            switch (subType)
            {
                case 1:
                    _stat.SetBattleData(40, 11);
                    _stat.SetMaxHpMpData(400, 111);
                    _stat.SetMoveData(11, 5);
                    return _stat;
                case 2:
                    _stat.SetBattleData(20, 11);
                    _stat.SetMaxHpMpData(50, 111);
                    _stat.SetMoveData(0.4f, 1);
                    _stat.SetAttackData(15, 2 , eAttackType.Melee);
                    return _stat;
                case 3:
                    _stat.SetBattleData(30, 30);
                    _stat.SetMaxHpMpData(50, 111);
                    _stat.SetMoveData(1f, 1);
                    _stat.SetAttackData(45, 2 , eAttackType.Melee);
                    return _stat;
                case 4:
                    _stat.SetBattleData(40, 11);
                    _stat.SetMaxHpMpData(30, 111);
                    _stat.SetMoveData(1f, 1);
                    _stat.SetAttackData(40, 40, eAttackType.Range);
                    return _stat;
            }
        }
        return null;
    }

    static List<CharacterTable> _tabelList;
    public static CharacterTable GetCharacterTable(int index)
    {
        if (_tabelList == null)
        {
            _tabelList = new List<CharacterTable>();
            CharacterTable table1 = new CharacterTable("FHero", 1, eEntityLookDir.Right);
            CharacterTable table2 = new CharacterTable("Skeleton", 2, eEntityLookDir.Left);
            CharacterTable table3 = new CharacterTable("SkeletonClothed", 3, eEntityLookDir.Left);
            CharacterTable table4 = new CharacterTable("ghost", 4, eEntityLookDir.Right);
            _tabelList.Add(table1);
            _tabelList.Add(table2);
            _tabelList.Add(table3);
            _tabelList.Add(table4);
        }
        foreach (var ob in _tabelList)
        {
           if(ob._tabelNumber == index)
            {
                return ob;
            }
        }
        return null;
    }
    static List<AsssetBundleLoadTable> _bundleTableList =null;
    public static List<AsssetBundleLoadTable> GetBundleLoadTable(int loadIndx)
    {
        if(_bundleTableList == null)
        {
            _bundleTableList = new List<AsssetBundleLoadTable>();
            _bundleTableList.Add(CreateLoadTable(0, eBundleLoadType.Static, 0, "cemetery"));
            _bundleTableList.Add(CreateLoadTable(1, eBundleLoadType.Static, 0, "sound"));
            _bundleTableList.Add(CreateLoadTable(2, eBundleLoadType.Dynamic, 2, "f_hero"));
            _bundleTableList.Add(CreateLoadTable(3, eBundleLoadType.Dynamic, 2, "monster"));
            _bundleTableList.Add(CreateLoadTable(4, eBundleLoadType.Dynamic, 2, "fx"));
        }
        List<AsssetBundleLoadTable> list = new List<AsssetBundleLoadTable>();
        foreach (var ob in _bundleTableList)
        {
            if(ob._loadIndex == loadIndx)
            {
                list.Add(ob);
            }
        }

        return _bundleTableList;
    }

    static List<SoundData> _soundList;
    public static SoundData GetSoundDate(string assetName)
    {
        if(_soundList == null)
        {
            _soundList = new List<SoundData>();
            SoundData data = new SoundData(eSoundType.Background, "119 mortvia aqueduct");
            SoundData data1 = new SoundData(eSoundType.Fireball, "Fireball+1");
            SoundData data2 = new SoundData(eSoundType.Walk, "Grass_short_02");
            SoundData data3 = new SoundData(eSoundType.Hit, "zapsplat_horror_kitchen_knife_stab_body_twist_19463");
            SoundData data4 = new SoundData(eSoundType.Slash, "zapsplat_warfare_sword_swing_fast_whoosh_metal_003");
            SoundData data5 = new SoundData(eSoundType.Punch, "punch");
            

            _soundList.Add(data);
            _soundList.Add(data1);
            _soundList.Add(data2);
            _soundList.Add(data3);
            _soundList.Add(data4);
            _soundList.Add(data5);
        }

        foreach (var ob in _soundList)
        {
            if (ob._assetName == assetName)
            {
                return ob;
            }
        }

        return null;
    }
    static AsssetBundleLoadTable CreateLoadTable(int tableIndex, eBundleLoadType type, int loadIndex, string bundleName)
    {
        AsssetBundleLoadTable data = new AsssetBundleLoadTable();
        data._tableIndex = tableIndex;
        data._loadIndex = loadIndex;
        data._bundleType = type;
        data._bundleName = bundleName;
        return data;
    }

    static List<BattleInfo> _battleAssetList;
    public static List<BattleInfo> GetBaettleInfo()
    {
        if(_battleAssetList == null)
        {
            _battleAssetList = new List<BattleInfo>();
            BattleInfo info1 = new BattleInfo("f_hero", "FHero", eAssetTypeInGame.Unit);
            BattleInfo info2 = new BattleInfo("cemetery", "Cametery_TileMap", eAssetTypeInGame.Map);
            BattleInfo info3 = new BattleInfo("fx", "CFX4 Aura Bubble C", eAssetTypeInGame.Fx);
            BattleInfo info4 = new BattleInfo("fx", "CFX4 Aura Bubble C", eAssetTypeInGame.Fx);
            BattleInfo info5 = new BattleInfo("fx", "CFX4 Aura Bubble C", eAssetTypeInGame.Fx);
            BattleInfo info6 = new BattleInfo("fx", "fx_zap", eAssetTypeInGame.Fx);
            BattleInfo info7 = new BattleInfo("fx", "fx_zap", eAssetTypeInGame.Fx);
            BattleInfo info8 = new BattleInfo("fx", "fx_zap", eAssetTypeInGame.Fx);
            BattleInfo info9 = new BattleInfo("fx", "fx_zap", eAssetTypeInGame.Fx);
            

            BattleInfo info10 = new BattleInfo("sound", "119 mortvia aqueduct", eAssetTypeInGame.SFX);
            BattleInfo info11 = new BattleInfo("sound", "Fireball+1", eAssetTypeInGame.SFX);
            BattleInfo info12 = new BattleInfo("sound", "Grass_short_02", eAssetTypeInGame.SFX);
            BattleInfo info13 = new BattleInfo("sound", "zapsplat_horror_kitchen_knife_stab_body_twist_19463", eAssetTypeInGame.SFX);
            BattleInfo info14 = new BattleInfo("sound", "zapsplat_warfare_sword_swing_fast_whoosh_metal_003", eAssetTypeInGame.SFX);
            BattleInfo info15 = new BattleInfo("sound", "punch", eAssetTypeInGame.SFX);


            BattleInfo info16 = new BattleInfo("fx", "CFX4 Fire", eAssetTypeInGame.Fx);
            BattleInfo info17 = new BattleInfo("fx", "CFX4 Fire", eAssetTypeInGame.Fx);
            BattleInfo info18 = new BattleInfo("fx", "CFX4 Fire", eAssetTypeInGame.Fx);
            BattleInfo info19 = new BattleInfo("fx", "CFX4 Fire", eAssetTypeInGame.Fx);

            _battleAssetList.Add(info1);
            _battleAssetList.Add(info2);
            _battleAssetList.Add(info3);
            _battleAssetList.Add(info4);
            _battleAssetList.Add(info5);
            _battleAssetList.Add(info6);
            _battleAssetList.Add(info7);
            _battleAssetList.Add(info8);
            _battleAssetList.Add(info9);
            _battleAssetList.Add(info10);
            _battleAssetList.Add(info11);
            _battleAssetList.Add(info12);
            _battleAssetList.Add(info13);
            _battleAssetList.Add(info14);
            _battleAssetList.Add(info15);
            _battleAssetList.Add(info16);
            _battleAssetList.Add(info17);
            _battleAssetList.Add(info18);
            _battleAssetList.Add(info19);
            for (int i = 0; i < 6; i++)
            {
                BattleInfo info = new BattleInfo("monster", "Skeleton", eAssetTypeInGame.Unit);
                _battleAssetList.Add(info);
            }
            for (int i = 0; i < 3; i++)
            {
                BattleInfo info = new BattleInfo("monster", "SkeletonClothed", eAssetTypeInGame.Unit);
                _battleAssetList.Add(info);
            }
            for (int i = 0; i < 7; i++)
            {
                BattleInfo info = new BattleInfo("monster", "ghost", eAssetTypeInGame.Unit);
                _battleAssetList.Add(info);
            }

            for (int i = 0; i < 7; i++)
            {
                BattleInfo info = new BattleInfo("monster", "fireball", eAssetTypeInGame.Unit);
                _battleAssetList.Add(info);
            }

         

        }


        return _battleAssetList;
    }
   static List<ConditionData> _list;
    public static ConditionData GetConditionData(eCondition type)
    {
        _list = new List<ConditionData>();
        _list.Add(new ConditionData(eCondition.Immune, 2, 1, 0 , false));

        return _list[0];
    }
}

public class BattleInfo
{
    public string _bundleName;
    public string _assetName;
    public eAssetTypeInGame _assetType;
    public BattleInfo(string bundleName , string assetName, eAssetTypeInGame type)
    {
        _bundleName = bundleName;
        _assetName = assetName;
        _assetType = type;
    }
}

public class AsssetBundleLoadTable
{
    public int _tableIndex;
    public int _loadIndex;
    public eBundleLoadType _bundleType;
    public string _bundleName;

}
public class ConditionData
{
    public eCondition _condition;
    public float _duration;
    public int _timeTick;
    public float _conditionValue;
    public bool _isAddtive;
    public ConditionData(eCondition condition , float duration , int timeTick , float value ,bool isAddtive)
    {
        _condition = condition;
        _duration = duration;
        _timeTick = timeTick;
        _conditionValue = value;
        _isAddtive = isAddtive;
    }
}

public class SoundData
{
    public eSoundType _type;
    public string _assetName;
    public SoundData( eSoundType type, string assetName)
    {
        _type = type;
        _assetName = assetName;
    }
}
