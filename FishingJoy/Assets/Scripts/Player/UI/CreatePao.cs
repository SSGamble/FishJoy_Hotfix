using UnityEngine;
using System.Collections;

/// <summary>
/// 产生UI泡泡
/// </summary>
public class CreatePao : MonoBehaviour {

    public GameObject pao;
    public Transform panel;
    private float timeVal = 6; // 计时器


    void Start() {

    }

    void Update() {
        // 计时，生成泡泡
        if (timeVal >= 6) {
            for (int i = 0; i < 4; i++) { // 生成 4 个泡泡
                Invoke("InstPao", 1); // 每间隔一秒，产生一个泡泡
            }
            timeVal = 0;
        }
        else {
            timeVal += Time.deltaTime;
        }
    }

    /// <summary>
    /// 生成泡泡
    /// </summary>
    private void InstPao() {

        GameObject itemGo = Instantiate(pao, transform.position, Quaternion.Euler(0, 0, Random.Range(-80, 0))) as GameObject;
        itemGo.transform.SetParent(panel);
    }
}
