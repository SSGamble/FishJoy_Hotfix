using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 捕到贝壳的道具卡
/// </summary>
public class Card : MonoBehaviour {

    private Transform playerTransform;

    public int num;

    public Sprite[] cards;

    private SpriteRenderer sr;

    private AudioSource audios;

    void Start() {
        Destroy(this.gameObject, 1);
        audios = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = cards[num];
        audios.Play();
    }
}
