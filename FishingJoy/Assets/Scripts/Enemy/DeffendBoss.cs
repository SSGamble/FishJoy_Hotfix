using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 有护盾的boss
/// </summary>
[Hotfix]
public class DeffendBoss : Boss {

    private bool isDeffend = false;

    private float deffendTime = 0;

    public GameObject deffend;

    [LuaCallCSharp]
    void Start() {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        playerTransform = Gun.Instance.transform;
    }

    void Update() {
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
        //灼烧效果
        if (Gun.Instance.Fire) {
            fire.SetActive(true);
        }
        else {
            fire.SetActive(false);
        }
        if (Gun.Instance.Ice) {
            return;
        }
        //boss的行为方法
        Attack(m_reduceGold, m_reduceDiamond);
        if (!isAttack) {
            fishMove();
        }
        //保护方法
        if (deffendTime >= 10) {
            deffendTime = 0;
            DeffenMe();
        }
        else {
            deffendTime += Time.deltaTime;
        }
    }

    void DeffenMe() {
        isDeffend = true;
        deffend.SetActive(true);
        Invoke("CloseDeffendMe", 3);
    }

    private void CloseDeffendMe() {
        deffend.SetActive(false);
        isDeffend = false;
    }

    public override void TakeDamage(int attackValue) {
        if (isDeffend) {
            return;
        }
        base.TakeDamage(attackValue);
    }
}
