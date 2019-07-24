using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// boss脚本
/// </summary>
[Hotfix]
public class Boss : MonoBehaviour {

    public int hp = 50;
    public GameObject deadEeffect;
    public int GetGold = 10;
    public int GetDiamands = 10;
    public GameObject diamands;
    public GameObject gold;
    public float moveSpeed = 2;
    protected int m_reduceGold;
    protected int m_reduceDiamond;

    protected Transform playerTransform;

    protected GameObject fire;
    protected GameObject ice;
    protected Animator iceAni;
    protected Animator gameObjectAni;
    protected AudioSource bossAudio;

    //计时器
    private float rotateTime;
    private float timeVal;

    protected bool hasIce;
    protected bool isAttack;

    // 此方法其他部分都是正确且需要执行的，只需要修改那一行代码就行了，即先执行已有的逻辑再执行附加内容
    [LuaCallCSharp]
    void Start() {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        playerTransform = Gun.Instance.transform;
        m_reduceGold = 10;  // 此处应该修改为 -20
        m_reduceDiamond = 0;
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
    }

    [LuaCallCSharp]
    public virtual void TakeDamage(int attackValue) {
        if (Gun.Instance.Fire) {
            attackValue *= 2;
        }

        hp -= attackValue;
        if (hp <= 0) {
            Instantiate(deadEeffect, transform.position, transform.rotation);
            Gun.Instance.GoldChange(GetGold * 10);
            Gun.Instance.DiamandsChange(GetDiamands * 10);
            // 钻石和金币穿插生成
            for (int i = 0; i < 11; i++) {
                GameObject itemGo = Instantiate(gold, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 18 + 36 * (i - 1), 0)));
                itemGo.GetComponent<Gold>().bossPrize = true;
            }
            for (int i = 0; i < 11; i++) {
                GameObject itemGo = Instantiate(diamands, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 36 + 36 * (i - 1), 0)));
                itemGo.GetComponent<Gold>().bossPrize = true;
            }
            Destroy(this.gameObject);
        }
    }

    public void fishMove() {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (rotateTime >= 5) {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else {
            rotateTime += Time.deltaTime;
        }
    }

    public void Attack(int reduceGold, int reduceDiamond) {
        if (timeVal > 20) {
            transform.LookAt(playerTransform);
            transform.eulerAngles += new Vector3(90, -90, 0);
            isAttack = true;
            timeVal = 0;
        }
        else {
            timeVal += Time.deltaTime;
        }
        if (isAttack) {
            gameObjectAni.SetBool("isAttack", true);
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 1 / Vector3.Distance(transform.position, playerTransform.position) * Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, playerTransform.position) <= 4) {
                if (reduceGold != 0) {
                    Gun.Instance.GoldChange(reduceGold);
                }
                if (reduceDiamond != 0) {
                    Gun.Instance.DiamandsChange(reduceDiamond);
                }

                gameObjectAni.SetBool("isAttack", false);
                isAttack = false;
                Gun.Instance.BossAttack();
                rotateTime = 0;
                Invoke("ReturnAngle", 4);
            }
        }
    }

    public void ReturnAngle() {
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
