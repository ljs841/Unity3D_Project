using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Battle_Scene : MonoBehaviour
{

    public Transform scrollParent;
    private void Awake()
    {
        UIManager._Instance.SceneForCameraSetting();
        var ff = BattleManager._Instance.GetPlayer();
        for (int i = 0; i < 7; i++)
        {
            EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);
        }
        var obj = AssetBundleManager.Instance.GetAsset<GameObject>("cemetery", "MapScroll");

        GameObject ob = Instantiate(obj);
        ob.transform.SetParent(scrollParent);
        ob.transform.localPosition = Vector3.zero;
       
        var sc = UIManager._Instance.CreateUIPrefab<Battle_Control>(ConstValue._battle, eUILayer.Layer1);
        sc.Show();
        ff.StartEntity();

       SoundManager.Instance.PlayBgm();

    }
}
