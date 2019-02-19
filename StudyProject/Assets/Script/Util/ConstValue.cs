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
        public static string _unitAniContiner = "AniContainer";
        public static string _unitNode = "PartPivot";

        public static string _grayScaleShadeName = "Hidden/GrayScale";
        public static Vector3 _screenOutPos = new Vector3(9999, 9999, 9999);
        public static float _characterTypeLogicInterval = Time.deltaTime;
        public static float _defaultTypeLogicInterval = 0.33f;

        //physics Ground Check
        public static float _minGroundNormalY = .65f;
        public static float _groundCastRayDistance = 1.0f;
        public static float _forwardCastRayDistance = 0.5f;
        public static float _limitGravity = Physics2D.gravity.y *0.7f;
        public static float _minGroundDistance = 0.15f;
        public static float _minMoveDistance = 0.001f;
        public static float _minVelocity_Y = -0.2f;
        public static float _checkxGroundDistance = 0.4f;




    }
    
}
