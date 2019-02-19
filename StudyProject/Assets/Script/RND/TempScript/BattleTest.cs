using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class BattleTest : MonoBehaviour
{
    public GameObject _obj;
    public GameObject _obj1;
    public GameObject _obj2;
    public GameObject _obj3;
    public GameObject _obj6;
    public GameObject _obj7;
    public GameObject _obj8;
    public GameObject _obj9;
    public GameObject _obj10;
    public GameObject _fx;
    public GameObject _fx1;
    public GameObject _fx11;
    public GameObject _fx112;
    public GameObject _fx2;
    public GameObject _fx3;
    public GameObject _fx4;
    public GameObject _fx5;
    public GameObject _fx6;
    public GameObject _fx7;
    public GameObject _fx8;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    // Start is called before the first frame update
    void Awake()
    {
        
        UnitObjectPool.Instance.AddResources("FHero" ,_obj);

        for(int i = 0; i<6; i++)
        {
            UnitObjectPool.Instance.AddResources("Skeleton", Instantiate(_obj1));
        }
        for (int i = 0; i < 3; i++)
        {
            UnitObjectPool.Instance.AddResources("SkeletonClothed", Instantiate(_obj2));
        }
        for (int i = 0; i < 7; i++)
        {
            UnitObjectPool.Instance.AddResources("ghost", Instantiate(_obj3));
        }
        SoundManager.Instance.AddClip("119 mortvia aqueduct", clip1);
        SoundManager.Instance.AddClip("Fireball+1", clip2);
        SoundManager.Instance.AddClip("Grass_short_02", clip3);
        SoundManager.Instance.AddClip("zapsplat_horror_kitchen_knife_stab_body_twist_19463", clip4);
        SoundManager.Instance.AddClip("zapsplat_warfare_sword_swing_fast_whoosh_metal_003", clip5);
        SoundManager.Instance.AddClip("punch", clip6);
        UnitObjectPool.Instance.AddResources("fireball", _obj6);
        UnitObjectPool.Instance.AddResources("fireball", _obj7);
        UnitObjectPool.Instance.AddResources("fireball", _obj8);
        UnitObjectPool.Instance.AddResources("fireball", _obj9);
        UnitObjectPool.Instance.AddResources("fireball", _obj10);

        FxManager.Instance.AddResoruces(eFxType.Immune, _fx);
        FxManager.Instance.AddResoruces(eFxType.Immune, _fx11);
        FxManager.Instance.AddResoruces(eFxType.Immune, _fx112);
        FxManager.Instance.AddResoruces(eFxType.Hit1, _fx1);
        FxManager.Instance.AddResoruces(eFxType.Hit1, _fx2);
        FxManager.Instance.AddResoruces(eFxType.Hit1, _fx3);
        FxManager.Instance.AddResoruces(eFxType.Hit1, _fx4);
        FxManager.Instance.AddResoruces(eFxType.DIe, _fx5);
        FxManager.Instance.AddResoruces(eFxType.DIe, _fx6);
        FxManager.Instance.AddResoruces(eFxType.DIe, _fx7);
        FxManager.Instance.AddResoruces(eFxType.DIe, _fx8);
        var ff = BattleManager._Instance.GetPlayer();

        var sc = UIManager._Instance.CreateUIPrefab<Battle_Control>(ConstValue._battle, eUILayer.Layer1);

        UIManager._Instance.SceneForCameraSetting();
        sc.Show();
        ff.StartEntity();

        EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);
        EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);
        EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);
        EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);
        EntityFactory._Instance.CreateEntityForBattle("fireball", eEntityType.InGameProjectile, eEntityLookDir.Left, 1);

        //Character enemy1 = (Character)EntityFactory._Instance.CreateEntityForBattle("SkeletonClothed", eEntityType.InGameCharacter, eEntityLookDir.Left, 3);
        //enemy1.StartEntity();.
        //SoundManager.Instance.PlayBgm();
    }
    /*
    
    float deltaTime = 0;
     private void Update()
     {
        //사인 함수에 파이에 타임변화량을 곱하면
        //파이값에 도달할떄까지를 시간변화량을 곱해 알아낸다.
        //파이값에 도달할떄까지 1초 그러면 0.5초면 가장높은 값 1초면 지면
        
         var height = Mathf.Sin(Mathf.PI * deltaTime / 2);
         Debug.Log(string.Format("height = {0} , deltaTime = {1}",height , deltaTime));
         deltaTime += Time.deltaTime;
     }
     */
}
