using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConstValues
{
    public enum eSceneName
    {
        CI = 0,
        Patch,
        Loading,
        Login,
        TwoDTest,
        Battle,


        None,

    }

    public class ConstValue
    {
        public static string _baseLayer = "UI/Prefab/MainUI";
        public static string _cI = "UI/Prefab/UI_CI";
        public static string _patch = "UI/Prefab/UI_Patch";
        public static string _loading = "UI/Prefab/UI_Loading";
        public static string _login = "UI/Prefab/UI_Login";
        public static string _battle = "UI/Prefab/BattleUI";
        public static string _scrolTest = "UI/Prefab/ScrollTest";




        public static string _charHero = "Character/Hero/Hero";


        public static string _grayScaleShadeName = "Hidden/GrayScale";
        public static Vector3 _screenOutPos = new Vector3(9999, 9999, 9999);
        public static float _characterTypeLogicInterval = 0.1f;
        public static float _defaultTypeLogicInterval = 0.33f;




    }
    
}
