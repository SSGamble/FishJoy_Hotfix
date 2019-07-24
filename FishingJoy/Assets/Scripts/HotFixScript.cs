using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

/// <summary>
/// 热更新脚本，用于加载 lua 脚本
/// </summary>
public class HotFixScript : MonoBehaviour {

    private LuaEnv luaEnv;

    /// <summary>
    /// LuaEnv 的实例化应该是走在最前面的，而不仅仅是在 awake 里，这里只是为了演示，若使用了框架什么的，需要注意这个问题
    /// </summary>
    private void Awake() {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader); // 添加 Loader
        luaEnv.DoString("require 'fish'"); // 利用 Loader 加载
    }

    private void Start() {

    }

    private void OnDisable() {
        luaEnv.DoString("require 'fishDispose'"); // 释放，反注册
    }

    void OnDestroy() {
        luaEnv.Dispose();
    }

    /// <summary>
    /// 自定义 Loader，用于自定义 lua 脚本的路径
    /// </summary>
    private byte[] MyLoader(ref string filePath) {
        string absParh = @"G:\UnityDocuments\FishingJoy\LuaPackage\" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absParh)); // 将 Lua 程序转换为了字节数组
    }

    // ---------------------------------- 加载 AB ---------------------------------------

    // 预制体字典
    public static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    // 本地加载
    [LuaCallCSharp]
    public static void LoadResource(string resName, string filePath) {
        AssetBundle ab = AssetBundle.LoadFromFile(@"G:\UnityDocuments\FishingJoy\Assets\AssetBundles\" + filePath);
        GameObject gameObject = ab.LoadAsset<GameObject>(resName);
        prefabDict.Add(resName, gameObject);
    }

    // 网络加载
    //[LuaCallCSharp]
    //public void LoadResource(string resName, string filePath) {
    //    StartCoroutine(LoadResourceCorotine(resName, filePath));
    //}

    /// <summary>
    /// 从网络加载 AB 包
    /// </summary>
    //IEnumerator LoadResourceCorotine(string resName, string filePath) {
    //    UnityWebRequest request = UnityWebRequest.GetAssetBundle(@"http://localhost:1915/AssetBundles/" + filePath);
    //    yield return request.SendWebRequest();
    //    AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
    //    GameObject gameObject = ab.LoadAsset<GameObject>(resName);
    //    prefabDict.Add(resName, gameObject);
    //}

    /// <summary>
    /// 取游戏物体
    /// </summary>
    [LuaCallCSharp]
    public static GameObject GetGameObject(string goName) {
        return prefabDict[goName];
    }
}