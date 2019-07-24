using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水纹播放的特效，一些不能直接做成动画的图片，可以直接使用代码播放
/// </summary>
public class Water : MonoBehaviour {

    private SpriteRenderer sr;

    public Sprite[] pictures;

    private int count = 0;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 逐帧渲染图片
    /// </summary>
    void Update() {
        sr.sprite = pictures[count];
        count++;
        if (count == pictures.Length) {
            count = 0;
        }
    }
}
