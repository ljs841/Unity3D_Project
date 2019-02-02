using System;
using System.Collections.Generic;

public class BattleManager
{
    /// <summary>
    /// battle manager는 오직 전투에 관해서만 관리 한다.
    /// 전투는 전투씬에서 행해지는 모든 것 그것에 대한 사전 데이터 셋팅까지
    /// 전투에 필요한 데이터 셋팅 및 셋팅된 데이터 제공
    /// 전투에사용되는 인풋 컨트롤러
    /// 전투의 흐름을 관리하는 배틀씬 관리자
    /// </summary>
    private static BattleManager _instance;
    public static BattleManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleManager();
            }
            return _instance;
        }
    }

    private InputManager _inputManager;
    public InputManager InputManager
    {
        get
        {
            if(_inputManager == null)
            {
                _inputManager = new InputManager();
            }
            return _inputManager;
        }
    }

    public BattleInfo GetBattleInfo()
    {
        return TempData.GetBaettleInfo();
    }


    Character _player;
    public void GetPlayer()
    {
        if(_player == null)
        {
            _player = (Character)EntityFactory._Instance.CreateEntityForBattle(eEntityType.InGameCharacter, eEntityLookDir.Right, 1);
            _player.PublishInputEvent(InputManager);
        }

    }
}
       