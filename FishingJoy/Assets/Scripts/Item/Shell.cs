using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贝壳
/// </summary>
public class Shell : MonoBehaviour {
    //计时器
    private float rotateTime;
    private float timeVal = 0;//无敌状态计时器

    //属性
    public float moveSpeed = 5;

    //开关
    private bool isDeffend = true;
    private bool hasIce = false;

    //引用
    public GameObject card;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    private SpriteRenderer sr;
    private float timeVals;

    void Start() {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 10);
    }

    void Update() {
        if (timeVals >= 9) {
            sr.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else {
            timeVals += Time.deltaTime;
        }
        //灼烧效果
        if (Gun.Instance.Fire) {
            fire.SetActive(true);

        }
        else {
            fire.SetActive(false);
        }
        //冰冻效果
        if (Gun.Instance.Ice) {
            gameObjectAni.enabled = false;
            ice.SetActive(true);
            if (!hasIce) {
                iceAni.SetTrigger("Ice");
                hasIce = true;
            }
        }
        else {
            gameObjectAni.enabled = true;
            hasIce = false;
            ice.SetActive(false);
        }
        if (Gun.Instance.Ice) {
            return;
        }
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (rotateTime >= 5) {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else {
            rotateTime += Time.deltaTime;
        }
        if (timeVal < 1) {

            timeVal += Time.deltaTime;
        }
        else if (timeVal >= 1 && timeVal < 1.5) {

            timeVal += Time.deltaTime;
            isDeffend = false;
        }
        else if (timeVal >= 1.5) {

            isDeffend = true;
            timeVal = 0;
        }
    }

    public void GetEffects() {
        if (isDeffend) {
            return;
        }
        else {
            int num = Random.Range(0, 3);
            switch (num) {
                case 0:
                    Gun.Instance.CanShootForFree();
                    break;
                case 1:
                    Gun.Instance.CanGetDoubleGold();
                    break;
                case 2:
                    Gun.Instance.CanShootNoCD();
                    break;
                default:
                    break;
            }
            GameObject go = Instantiate(card, transform.position, card.transform.rotation) as GameObject;
            go.GetComponent<Card>().num = num;
            Destroy(this.gameObject);
        }
    }
}
