using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 产鱼器
/// </summary>
//[Hotfix]
public class CreateFish : MonoBehaviour {

    //引用
    public GameObject[] fishList;
    public GameObject[] item;
    public GameObject boss;
    public GameObject boss2;
    public GameObject boss3;
    public Transform[] CreateFishPlace;

    private float ItemtimeVal = 0;//游戏物体计时器
    private float createManyFish;
    private float timeVals = 0;

    //成员变量
    private int num;
    private int ItemNum;
    private int placeNum;
    private int CreateMorden;

    public HotFixScript hotFixScript;
    void Start() {
        
    }

    //x:-26  -   26
    //z:-16  -   16
    private void Awake() {

    }

    [LuaCallCSharp]
    void Update() {
        //鱼群的生成
        CreateALotOfFish();
        //单种鱼的生成
        if (ItemtimeVal >= 0.5) {
            //位置随机数
            num = Random.Range(0, 4);
            //游戏物体随机数
            ItemNum = Random.Range(1, 101);
            //产生气泡
            if (ItemNum < 20) {
                CreateGameObject(item[3]);
                CreateGameObject(fishList[6]);
            }
            //贝壳10% 85-94 
            //第一种鱼42% 42
            if (ItemNum <= 42) {
                CreateGameObject(fishList[0]);
                CreateGameObject(item[0]);
                CreateGameObject(fishList[3]);
                CreateGameObject(item[0]);
            }
            //第二种鱼30% 43-72
            else if (ItemNum >= 43 && ItemNum < 72) {
                CreateGameObject(fishList[1]);
                CreateGameObject(item[0]);
                CreateGameObject(fishList[4]);
            }
            //第三种鱼10% 73-84
            else if (ItemNum >= 73 && ItemNum < 84) {
                CreateGameObject(fishList[2]);
                CreateGameObject(fishList[5]);
            }
            //第一种美人鱼5%，第二种3%  95-98  99-100
            else if (ItemNum >= 94 && ItemNum <= 98) {
                CreateGameObject(item[1]);
            }
            else if (ItemNum >= 84 && ItemNum < 86) {
                CreateGameObject(boss2);
            }
            else if (ItemNum > 98 && ItemNum < 100) {
                CreateGameObject(item[2]);
                CreateGameObject(boss);
            }
            else {
                CreateGameObject(item[0]);
                CreateGameObject(boss3);
            }
            ItemtimeVal = 0;
        }
        else {
            ItemtimeVal += Time.deltaTime;
        }
    }

    //生成鱼群
    private void CreateALotOfFish() {
        if (createManyFish >= 15) {
            if (CreateMorden == 2) {
                GameObject go = fishList[Random.Range(2, fishList.Length)];
                for (int i = 0; i < 11; i++) {
                    GameObject itemGo = Instantiate(go, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
                    itemGo.GetComponent<Fish>().cantRotate = true;
                }
                createManyFish = 0;
            }
            else if (CreateMorden == 0 || CreateMorden == 1) {
                createManyFish += Time.deltaTime;
                if (createManyFish >= 18) {
                    createManyFish = 0;
                }
                if (timeVals >= 0.2f) {
                    int num = Random.Range(0, 2);
                    GameObject itemGo = Instantiate(fishList[num], CreateFishPlace[placeNum].position + new Vector3(0, 0, Random.Range(-2, 2)), CreateFishPlace[placeNum].rotation);
                    itemGo.GetComponent<Fish>().cantRotate = true;
                    timeVals = 0;
                }
                else {
                    timeVals += Time.deltaTime;
                }
            }
        }
        else {
            createManyFish += Time.deltaTime;
            placeNum = Random.Range(0, 2);
            CreateMorden = Random.Range(0, 3);
        }
    }

    private void CreateFishs(GameObject go) {
        Instantiate(go, RandomPos(num), Quaternion.Euler(go.transform.eulerAngles));
    }

    //产生游戏物体
    private void CreateGameObject(GameObject go) {
        Instantiate(go, RandomPos(num), Quaternion.Euler(RandomAngle(num) + go.transform.eulerAngles));
    }

    //随机位置
    private Vector3 RandomPos(int num) {
        Vector3 Vpositon = new Vector3();

        switch (num) {
            case 0:
                Vpositon = new Vector3(-24, 1, Random.Range(-14f, 14f));//-30  -  30
                break;
            case 1:
                Vpositon = new Vector3(Random.Range(-24f, 24f), 1, 14);//60 - 120
                break;
            case 2:
                Vpositon = new Vector3(24, 1, Random.Range(-14f, 14f));//150-210
                break;
            case 3:
                Vpositon = new Vector3(Random.Range(-24f, 24f), 1, -14);//-60-  -120
                break;
            default:
                break;
        }
        return Vpositon;
    }

    //随机角度
    private Vector3 RandomAngle(int num) {
        Vector3 Vangle = new Vector3();
        switch (num) {
            case 0:
                Vangle = new Vector3(0, Random.Range(-30f, 30f), 0);//-30  -  30
                break;
            case 1:
                Vangle = new Vector3(0, Random.Range(60f, 120f), 0);//60 - 120
                break;
            case 2:
                Vangle = new Vector3(0, Random.Range(150f, 210f), 0);//150-210
                break;
            case 3:
                Vangle = new Vector3(0, Random.Range(-60f, -120f), 0);//-60-  -120
                break;
            default:
                break;
        }
        return Vangle;
    }
}