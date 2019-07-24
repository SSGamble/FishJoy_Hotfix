using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class LoadGame : MonoBehaviour {

    public Slider processView;

    void Start() {
        LoadGameMethod();
    }

    public void LoadGameMethod() {
        //StartCoroutine(LoadResourceCorotine());
        StartCoroutine(StartLoading_4(2));
    }

    /// <summary>
    /// 加载进度
    /// </summary>
    /// <param name="scene">当前需要加载的游戏场景</param>
    /// <returns></returns>
    private IEnumerator StartLoading_4(int scene) {
        int displayProgress = 0; // 当前展示的进度，向 toProgress 靠拢
        int toProgress = 0; // 当前想要到达的进度
        AsyncOperation op = SceneManager.LoadSceneAsync(scene); // 异步加载场景
        op.allowSceneActivation = false; // 让当前进度加载停在 0.9，其实会停在 0.89999 左右
        // 显示进度逐渐追到目标进度(0.9)
        while (op.progress < 0.9f) {
            toProgress = (int)op.progress * 100; // 目标进度
            while (displayProgress < toProgress) { // 显示进度累加
                ++displayProgress;
                SetLoadingPercentage(displayProgress); // 设置进度条
                yield return new WaitForEndOfFrame(); // 等待当前帧完结(GUI 等渲染结束)
            }
        }
        // 手动将显示进度设置为 100
        toProgress = 100;
        while (displayProgress < toProgress) {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true; // 继续加载，实际加载，异步加载场景最后 0.1 是很快的，可以忽略不计，所以可以先做显示处理
    }

    /// <summary>
    /// 给加载进度条设置进度数值
    /// </summary>
    /// <param name="v"></param>
    private void SetLoadingPercentage(float v) {
        processView.value = v / 100;
    }

    /// <summary>
    /// 加载 lua 文件
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadResourceCorotine() {
        UnityWebRequest request = UnityWebRequest.Get(@"http://localhost:1915/fish.lua.txt");
        yield return request.SendWebRequest();
        string str = request.downloadHandler.text;
        File.WriteAllText(@"D:\fish.lua.txt", str);
        UnityWebRequest request1 = UnityWebRequest.Get(@"http://localhost:1915/fishDispose.lua.txt");
        yield return request1.SendWebRequest();
        string str1 = request1.downloadHandler.text;
        File.WriteAllText(@"D:\fishDispose.lua.txt", str1);
    }
}
