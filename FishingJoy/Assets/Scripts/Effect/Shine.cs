using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 星星闪耀的特效
/// </summary>
public class Shine : MonoBehaviour {

    private Image img;
    public float speed = 4;
    private bool add;

    public void Awake() {
        img = GetComponent<Image>();
    }

    /// <summary>
    /// 星星，旋转和渐隐渐现效果
    /// </summary>
    void Update() {
        transform.Rotate(Vector3.forward * 4, Space.World); // 旋转
        if (!add) {
            img.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a <= 0.2f) {
                add = true;
            }
        }
        else {
            img.color += new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a >= 0.8f) {
                add = false;
            }
        }
    }
}
