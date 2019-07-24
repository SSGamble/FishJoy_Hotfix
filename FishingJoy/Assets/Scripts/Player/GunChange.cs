using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 切枪的按钮
/// </summary>
public class GunChange : MonoBehaviour {

    public bool add;
    private Button button;
    private Image image;
    public Sprite[] buttonSprites;//0.+   1.灰色的+  2.-  3.灰色的-

    void Start() {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(ChangeGunLevel);
        image = GetComponent<Image>();

    }

    void Update() {
        if (Gun.Instance.canChangeGun) {
            if (add) {
                image.sprite = buttonSprites[0];

            }
            else {
                image.sprite = buttonSprites[2];
            }
        }
    }

    public void ChangeGunLevel() {
        if (Gun.Instance.canChangeGun) {
            if (add) {
                Gun.Instance.UpGun();
            }
            else {
                Gun.Instance.DownGun();
            }
        }
    }

    public void ToGray() {
        if (add) {
            image.sprite = buttonSprites[1];

        }
        else {
            image.sprite = buttonSprites[3];

        }
    }
}
