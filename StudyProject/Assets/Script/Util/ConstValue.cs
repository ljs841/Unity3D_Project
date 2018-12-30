using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstValues
{
    public enum SceneName
    {
        CI = 0,
        Loading,
        Login,

    }

    namespace UIResPath
    {
        public class ConstValue
        {
            public static string _baseLayer = "UI/Prefab/MainUI";
            public static string _cI = "UI/Prefab/UI_CI";
            public static string _loading = "UI/Prefab/UI_Loading";
        }
    }

    
}
