using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 金币，钻石
/// </summary>
public class Gold : MonoBehaviour {

    public enum ThePlaceTo {
        gold,
        diamands,
        imageGold,
        imageDiamands
    }
    public ThePlaceTo thePlaceTo;
    private Transform playerTransform;
    public float moveSpeed = 3;
    public GameObject star2;

    private AudioSource audios;
    public AudioClip goldAudio;
    public AudioClip diamandsAudio;

    private float timeVal2;
    public float defineTime2;
    private float timeBecome;
    private float timeVal3;

    public bool bossPrize = false;
    private bool beginMove = false;
    // Use this for initialization
    private void Awake() {
        audios = GetComponent<AudioSource>();
        switch (thePlaceTo) {
            case ThePlaceTo.gold:
                playerTransform = Gun.Instance.goldPlace;
                audios.clip = goldAudio;
                break;
            case ThePlaceTo.diamands:
                playerTransform = Gun.Instance.diamondsPlace;
                audios.clip = diamandsAudio;
                break;
            case ThePlaceTo.imageGold:
                playerTransform = Gun.Instance.imageGoldPlace;
                audios.clip = goldAudio;
                break;
            case ThePlaceTo.imageDiamands:
                playerTransform = Gun.Instance.imageDiamandsPlace;
                audios.clip = diamandsAudio;
                break;
            default:
                break;
        }
        audios.Play();
    }

    void Update() {
        if (timeBecome >= 0.5f) {
            beginMove = true;
        }
        else {
            timeBecome += Time.deltaTime;
        }
        if (beginMove) {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 1 / Vector3.Distance(transform.position, playerTransform.position) * Time.deltaTime * moveSpeed);
            if (thePlaceTo == ThePlaceTo.imageDiamands || thePlaceTo == ThePlaceTo.imageGold) {
                if (Vector3.Distance(transform.position, playerTransform.position) <= 2) {
                    Destroy(this.gameObject);
                }
                return;
            }
            if (transform.position == playerTransform.position) {
                Destroy(this.gameObject);
            }
            timeVal2 = InistStar(timeVal2, defineTime2, star2);
        }
        else {
            transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, Time.deltaTime * 3);
            if (bossPrize) {
                if (timeVal3 <= 0.3f) {
                    timeVal3 += Time.deltaTime;
                    transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
                }
            }
        }
    }

    private float InistStar(float timeVals, float defineTimes, GameObject stars) {
        if (timeVals >= defineTimes) {
            Instantiate(stars, this.transform.position, Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + Random.Range(-40f, 40f)));
            timeVals = 0;
        }
        else {
            timeVals += Time.deltaTime;
        }
        return timeVals;
    }
}
