using System.IO;
using UnityEditor;

public class CreateAssetBundles {

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles() {
        string assetBundleDirectory = "Assets/AssetBundles"; // 包的输出路径
        if (!Directory.Exists(assetBundleDirectory)) { // 若路径不存在，则创建
            Directory.CreateDirectory(assetBundleDirectory);
        }
        // BuildPipeline：允许您以编程方式构建可从 Web 加载的播放器或 AssetBundle。
        // BuildAssetBundles()：打包，Build 出来的包是有平台限制的
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}