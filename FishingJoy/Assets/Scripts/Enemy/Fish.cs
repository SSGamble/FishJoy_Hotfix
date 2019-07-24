using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 普通鱼的类
/// </summary>
//[Hotfix]
public class Fish : MonoBehaviour {

    //属性
    public float moveSpeed = 2;
    public int GetCold = 10;
    public int GetDiamands = 10;
    public int hp = 5;

    //计时器
    private float rotateTime;
    private float timeVal;

    //引用
    public GameObject gold;
    public GameObject diamands;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    private SpriteRenderer sr;
    public GameObject pao;

    //开关
    private bool hasIce = false;
    public bool isnet;
    private bool isDead = false;
    public bool cantRotate = false;

    void Start() {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 20); // 20s 后自动销毁
    }

    void Update() {
        if (timeVal >= 14 || isDead) {
            sr.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else {
            timeVal += Time.deltaTime;
        }
        if (isDead) {
            return;
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
        //灼烧方法
        if (Gun.Instance.Fire) {
            fire.SetActive(true);
        }
        else {
            fire.SetActive(false);
        }
        if (Gun.Instance.Ice) {
            return;
        }
        if (isnet) {
            Invoke("Net", 0.5f);
            return;
        }
        fishMove();
    }
    public void Net() {
        if (isnet) {
            isnet = false;
        }
    }

    public void fishMove() {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (cantRotate) {
            return;
        }
        if (rotateTime >= 5) {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else {
            rotateTime += Time.deltaTime;
        }
    }

    [LuaCallCSharp]
    public void TakeDamage(int attackValue) {
        if (Gun.Instance.Fire) {
            attackValue *= 2;
        }
        hp -= attackValue;
        if (hp <= 0) {
            isDead = true;
            for (int i = 0; i < 9; i++) {
                Instantiate(pao, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
            }
            gameObjectAni.SetTrigger("Die");
            Invoke("Prize", 0.7f);
        }
    }

    private void Prize() {
        Gun.Instance.GoldChange(GetCold);
        if (GetDiamands != 0) {
            Gun.Instance.DiamandsChange(GetDiamands);
            Instantiate(diamands, transform.position, transform.rotation);
        }
        Instantiate(gold, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
