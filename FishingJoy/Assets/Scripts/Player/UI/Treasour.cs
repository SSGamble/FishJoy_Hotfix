using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 宝藏
/// </summary>
[Hotfix] // 热更新标签
public class Treasour : MonoBehaviour {

    private Button but;
    private Image img;

    public GameObject gold;
    public GameObject diamands;
    public GameObject cdView;

    public Transform cavas;
    private bool isDrease;


    private void Awake() {
        but = GetComponent<Button>();
        but.onClick.AddListener(OpenTreasour);
        img = GetComponent<Image>();
    }

    void OpenTreasour() {
        if (img.color.a != 1) {
            return;
        }
        cdView.SetActive(true);
        Gun.Instance.GoldChange(Random.Range(100, 200));
        Gun.Instance.DiamandsChange(Random.Range(10, 50));
        CreatePrize();
        isDrease = true;
    }

    [LuaCallCSharp]
    private void CreatePrize() {
        for (int i = 0; i < 5; i++) {
            GameObject go = Instantiate(gold, transform.position + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go.transform.SetParent(cavas);
            GameObject go1 = Instantiate(diamands, transform.position + new Vector3(0, 30, 0) + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go1.transform.SetParent(cavas);
        }
    }

    void Update() {
        if (isDrease) {
            img.color -= new Color(0, 0, 0, Time.deltaTime * 10);
            if (img.color.a <= 0.2) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                isDrease = false;
            }
        }
        else {
            img.color += new Color(0, 0, 0, Time.deltaTime * 0.01f);
            if (img.color.a >= 0.9) {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
                cdView.SetActive(false);
            }
        }
    }
}
