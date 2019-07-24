using UnityEngine;
using XLua;

namespace XLuaTest {

    [Hotfix] // 不建议使用这种方式，这里只是为了用于入门演示
    public class HotfixTest : MonoBehaviour {

        LuaEnv luaenv = new LuaEnv();

        void Update() {
            Debug.Log(">>>>>>>>Update in C#");
        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 300, 80), "Hotfix")) {
                // 用该 lua 方法替换 HotfixTest 类中的 Update 方法
                luaenv.DoString(@"
                xlua.hotfix(CS.XLuaTest.HotfixTest, 'Update', function(self)
                    print('HelloWorld <<<<<<<< 通过 Xlua 热更新输出')
                end)
                ");
            }
        }
    }
}
