using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using XLua;

// 如果涉及到 Assembly-CSharp.dll 之外的其它 dll，如下代码需要放到 Editor 目录
public static class HotfixCfg {

    // lua 中要使用到 C# 库的配置，比如 C# 标准库，或者 Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(System.Object),
                typeof(UnityEngine.Object),
                typeof(Vector3)
    };

    // C# 静态调用 Lua 的配置（包括事件的原型），仅可以配 delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator)
    };

    // 白名单
    [Hotfix]
    public static List<Type> HotfixClass = new List<Type>() {
                typeof(CreateFish),
                typeof(Fish)
    };

    // 将某名字空间下所有类配置到 Hotfix 列表
    //[Hotfix]
    //public static List<Type> by_property {
    //    get {
    //        return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
    //                where type.Namespace == "XXXX"
    //                select type).ToList();
    //    }
    //}

    // 黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"}
    };
}