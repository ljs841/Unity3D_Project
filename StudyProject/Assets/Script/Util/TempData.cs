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
                    _stat.SetBattleData(11, 11);
                    _stat.SetMaxHpMpData(111, 111);
                    _stat.SetMoveData(3, 1);
                    return _stat;
                case 2:
                    _stat.SetBattleData(11, 11);
                    _stat.SetMaxHpMpData(111, 111);
                    _stat.SetMoveData(1, 1);
                    return _stat;
            }
        }
        return null;
    }

    public static List<AsssetBundleLoadTable> GetBundleLoadTable(int loadIndx)
    {
        List<AsssetBundleLoadTable> list = new List<AsssetBundleLoadTable>();
        list.Add(CreateLoadTable(0, eBundleLoadType.Static, 0, "cametery"));
        list.Add(CreateLoadTable(1, eBundleLoadType.Dynamic, 1, "character"));

       

        return list;



    }
    static AsssetBundleLoadTable CreateLoadTable(int tableIndex , eBundleLoadType type , int loadIndex, string bundleName)
    {
        AsssetBundleLoadTable data = new AsssetBundleLoadTable();
        data._tableIndex = tableIndex;
        data._loadIndex = loadIndex;
        data._bundleType = type;
        data._bundleName = bundleName;
        return data;
    }


    public static BattleInfo GetBaettleInfo()
    {
        BattleInfo info = new BattleInfo();
        info.unitQueue = new Queue<string[]>();
        info.unitQueue.Enqueue(new string[2] { "character", "hero" });
        return info;
    }
}

public class BattleInfo
{
    public Queue<string[]> unitQueue;
}

public class AsssetBundleLoadTable
{
    public int _tableIndex;
    public int _loadIndex;
    public eBundleLoadType _bundleType;
    public string _bundleName;

}

