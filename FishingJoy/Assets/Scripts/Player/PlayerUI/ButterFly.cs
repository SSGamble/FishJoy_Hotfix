using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 散弹按钮
/// </summary>
[Hotfix]
public class ButterFly : MonoBehaviour {

    Button but;

    private float timeVal = 15;
    private bool canUse = true;
    private float totalTime = 15;
    public GameObject uiView;

    public Slider cdSlider;
    private int reduceDiamands;

    private void Awake() {
        but = transform.GetComponent<Button>();
        but.onClick.AddListener(Fire);

    }

    [LuaCallCSharp]
    void Start() {
        reduceDiamands = 10;
    }

    void Update() {
        if (timeVal >= 15) {
            timeVal = 15;
        }
        cdSlider.value = timeVal / totalTime;
        if (timeVal >= 15) {

            canUse = true;
            cdSlider.transform.Find("Background").gameObject.SetActive(false);
        }
        else {

            timeVal += Time.deltaTime;
        }
    }

    private void Fire() {
        if (canUse) {

            if (Gun.Instance.diamands <= reduceDiamands) {
                return;
            }
            Gun.Instance.DiamandsChange(-reduceDiamands);


            Gun.Instance.Butterfly = true;
            canUse = false;
            cdSlider.transform.Find("Background").gameObject.SetActive(true);
            timeVal = 0;
            Invoke("CloseFire", 8);
            uiView.SetActive(true);

        }

    }

    //关闭必杀的方法
    private void CloseFire() {
        uiView.SetActive(false);
        Gun.Instance.Butterfly = false;
    }


}
