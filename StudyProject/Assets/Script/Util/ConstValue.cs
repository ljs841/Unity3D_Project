using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConstValues
{
    public enum SceneName
    {
        CI = 0,
        Loading,
        Login,

    }

    public class ConstValue
    {
        public static string _baseLayer = "UI/Prefab/MainUI";
        public static string _cI = "UI/Prefab/UI_CI";
        public static string _loading = "UI/Prefab/UI_Loading";
        public static string _login = "UI/Prefab/UI_Login";
        public static string _scrolTest = "UI/Prefab/ScrollTest";

        

        public static Vector3 _screenOutPos = new Vector3(9999, 9999, 9999);




    }
    
}
