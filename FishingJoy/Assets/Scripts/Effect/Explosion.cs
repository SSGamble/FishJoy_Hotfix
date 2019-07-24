using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸特效
/// </summary>
public class Explosion : MonoBehaviour {

    public float DestoryTime = 0.2f;

    void Start() {
        Destroy(this.gameObject, DestoryTime);
    }

    void Update() {
        transform.localScale += new Vector3(Time.deltaTime * 10, Time.deltaTime * 10, Time.deltaTime * 10);
    }
}
