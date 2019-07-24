using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 枪
/// </summary>
[Hotfix]
public class Gun : MonoBehaviour {

    //属性
    public int gold = 100;
    public int diamands = 50;
    public int gunLevel = 1;
    private float rotateSpeed = 5f;
    public float attackCD = 1;
    private float GunCD = 4;
    public int level = 1;

    //引用
    public AudioClip[] bullectAudios;
    private AudioSource bullectAudio;
    public Transform attackPos;
    public GameObject[] Bullects;
    public GameObject net;
    public GunChange[] gunChange;

    public Transform goldPlace;
    public Transform diamondsPlace;
    public Transform imageGoldPlace;
    public Transform imageDiamandsPlace;

    public Text goldText;
    public Text diamandsText;

    //开关
    private bool canShootForFree = false;
    private bool canGetDoubleGold = false;
    public bool canShootNoCD = false;
    public bool canChangeGun = true;
    public bool bossAttack = false;
    public bool Fire = false;
    public bool Ice = false;
    public bool Butterfly = false;
    public bool attack = false;

    public bool changeAudio;

    private static Gun instance;
    public static Gun Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    private void Awake() {
        instance = this;
        gold = 1000;
        diamands = 1000;
        level = 2;
        bullectAudio = GetComponent<AudioSource>();
    }

    void Update() {
        goldText.text = gold.ToString();
        diamandsText.text = diamands.ToString();
        //旋转枪的方法
        RotateGun();
        if (GunCD <= 0) {
            canChangeGun = true;
            GunCD = 4;
        }
        else {
            GunCD -= Time.deltaTime;
        }
        //攻击的方法
        if (canShootNoCD) {
            Attack();
            attack = true;
            return;
        }

        if (attackCD >= 1 - gunLevel * 0.3) {
            Attack();
            attack = true;
        }
        else {
            attackCD += Time.deltaTime;
        }
    }

    /// <summary>
    /// 以下是方法的定义
    /// </summary>

    //旋转枪
    [LuaCallCSharp]
    private void RotateGun() {
        float h = Input.GetAxisRaw("Mouse Y");
        float v = Input.GetAxisRaw("Mouse X");
        transform.Rotate(-Vector3.forward * v * rotateSpeed);
        transform.Rotate(Vector3.forward * h * rotateSpeed);
        ClampAngle();
        //245,115
    }

    //换枪的方法
    public void UpGun() {
        gunLevel += 1;
        if (gunLevel == 4) {
            gunLevel = 1;
        }
        gunChange[0].ToGray();
        gunChange[1].ToGray();
        canChangeGun = false;
    }

    public void DownGun() {
        gunLevel -= 1;
        if (gunLevel == 0) {
            gunLevel = 3;
        }
        gunChange[0].ToGray();
        gunChange[1].ToGray();
        canChangeGun = false;
    }

    //限制角度
    private void ClampAngle() {
        float y = transform.eulerAngles.y;
        if (y <= 35) {
            y = 35;
        }
        else if (y >= 150) {
            y = 150;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    //攻击方法
    [LuaCallCSharp]
    private void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            bullectAudio.clip = bullectAudios[gunLevel - 1];
            bullectAudio.Play();
            if (Butterfly) { // 散弹枪
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, 20));
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, -20));
            }
            Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation);
            if (!canShootForFree) {
                GoldChange(-1 - (gunLevel - 1) * 2);
            }
            attackCD = 0;
            attack = false;
        }
    }

    //增减金钱
    [LuaCallCSharp]
    public void GoldChange(int number) {
        if (canGetDoubleGold) {
            if (number > 0) {
                number *= 2;
            }
        }
        gold += number;
    }

    //增减钻石
    [LuaCallCSharp]
    public void DiamandsChange(int number) {
        diamands += number;
    }

    /// <summary>
    /// 贝壳触发的一些效果方法
    /// </summary>
    public void CanShootForFree() {
        canShootForFree = true;
        Invoke("CantShootForFree", 5);
    }

    public void CantShootForFree() {
        canShootForFree = false;
    }

    public void CanGetDoubleGold() {
        canGetDoubleGold = true;
        Invoke("CantGetDoubleGold", 5);
    }

    public void CantGetDoubleGold() {
        canGetDoubleGold = false;
    }

    public void CanShootNoCD() {
        canShootNoCD = true;
        Invoke("CantShootNoCD", 5);
    }

    public void CantShootNoCD() {
        canShootNoCD = false;
    }

    //boss攻击的方法
    public void BossAttack() {
        bossAttack = true;
    }
}
