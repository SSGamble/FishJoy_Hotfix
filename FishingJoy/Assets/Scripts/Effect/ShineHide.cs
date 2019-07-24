using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 隐藏显示闪耀UI的特效,比如枪，星星
/// </summary>
public class ShineHide : MonoBehaviour {
    private float timeVal = 0;
    private bool isAdd = false;
    private Image img;
    public float defineTime = 3;

    void Awake() {
        img = GetComponent<Image>();

    }

    void Start() {

    }

    void Update() {
        timeVal += Time.deltaTime;
        if (!isAdd) {
            img.color -= new Color(0, 0, 0, Time.deltaTime * 5);
            if (timeVal > defineTime) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                isAdd = true;
                timeVal = 0;
            }
        }
        else {
            img.color += new Color(0, 0, 0, Time.deltaTime * 5);
            if (timeVal > defineTime) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
                isAdd = false;
                timeVal = 0;
            }

        }
    }
}







