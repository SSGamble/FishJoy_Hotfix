using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 会隐藏的boss
/// </summary>
[Hotfix]
public class InvisibleBoss : Boss {

    private bool isInvisible = false;

    private float invisibleTime = 0;
    private float recoverTime = 0;

    private BoxCollider box;
    private SpriteRenderer sr;

    [LuaCallCSharp]
    void Start() {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        box = GetComponent<BoxCollider>();
        sr = GetComponent<SpriteRenderer>();
        bossAudio = GetComponent<AudioSource>();
        playerTransform = Gun.Instance.transform;
    }

    // Update is called once per frame
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
        //隐形方法
        if (invisibleTime >= 10) {
            invisibleTime = 0;
            Invisible();
        }
        else {
            invisibleTime += Time.deltaTime;
        }
        if (isInvisible) {
            sr.color -= new Color(0, 0, 0, Time.deltaTime);
            box.enabled = false;
        }
        else {
            sr.color += new Color(0, 0, 0, Time.deltaTime);
            if (recoverTime >= 3) {
                recoverTime = 0;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            else {
                recoverTime += Time.deltaTime;
            }
            box.enabled = true;
        }
    }

    private void Invisible() {
        isInvisible = true;
        Invoke("CloseInvisible", 3);
    }

    private void CloseInvisible() {
        isInvisible = false;
    }
}
