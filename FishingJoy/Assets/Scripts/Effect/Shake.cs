using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// boss 攻击玩家产生的摄像机震动方法
/// </summary>
public class Shake : MonoBehaviour {

    private float cameraShake = 5; // 震动系数
    public GameObject UI; // 红色覆盖层

    void Update() {
        if (Gun.Instance.bossAttack) {
            UI.SetActive(true); 
            // 改变摄像机位置，形成震动效果
            transform.position = new Vector3((Random.Range(0f, cameraShake)) - cameraShake * 0.5f, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, (Random.Range(0f, cameraShake)) - cameraShake * 0.5f);
            cameraShake = cameraShake / 1.05f;
            if (cameraShake < 0.05f) {
                cameraShake = 0;
                UI.SetActive(false);
                Gun.Instance.bossAttack = false;
            }
        }
        else {
            cameraShake = 5;
        }
    }
}
