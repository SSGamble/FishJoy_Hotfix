using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 负责UI显示的枪
/// </summary>
[Hotfix]
public class GunImage : MonoBehaviour {

    public Sprite[] Guns;
    private Image img;

    public Transform idlePos;
    public Transform attackPos;

    private float rotateSpeed = 5f;


    private void Awake() {
        img = transform.GetComponent<Image>();
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //旋转枪的方法

        RotateGun();
        img.sprite = Guns[Gun.Instance.gunLevel - 1];


        //攻击的方法



        if (Gun.Instance.attack) {
            if (Input.GetMouseButtonDown(0)) {
                attack();
            }
        }

    }

    private void attack() {

        transform.position = Vector3.Lerp(transform.position, attackPos.position, 0.5f);
        Invoke("idle", 0.4f);
    }
    private void idle() {

        transform.position = Vector3.Lerp(transform.position, idlePos.position, 0.2f);

    }

    //旋转枪
    [LuaCallCSharp]
    private void RotateGun() {
        float h = Input.GetAxisRaw("Mouse Y");
        float v = Input.GetAxisRaw("Mouse X");
        transform.Rotate(-Vector3.forward * v * rotateSpeed);
        transform.Rotate(Vector3.forward * h * rotateSpeed);
        ClampAngle();
    }

    //限制角度
    private void ClampAngle() {
        float z = transform.eulerAngles.z;
        if (z <= 35) {
            z = 35;
        }
        else if (z >= 150) {
            z = 150;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
}
