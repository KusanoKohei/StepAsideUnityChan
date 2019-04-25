using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    //CarPrefabを入れる
    public GameObject carPrefab;
    //CoinPrefabを入れる
    public GameObject coinPrefab;
    //ConePrefabを入れる
    public GameObject conePrefab;
    //
    private float unitychanPosition;

    private float startPos;
    //ゴール地点
    private float goalPos;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //ユニティちゃんのインスタンス
    public GameObject unitychan;
    public float interval;



    // Use this for initialization
    void Start()
    {
        unitychan = GameObject.Find("unitychan");

        unitychanPosition = unitychan.transform.position.z;
        goalPos = unitychan.transform.position.z + 50;

    }

    // Update is called once per frame
    void Update()
    {
        if ((unitychanPosition + 40) < unitychan.transform.position.z)
        {

            //どのアイテムを出すかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, unitychan.transform.position.z+40);
                }
            }
            else
            {

                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, unitychan.transform.position.z + 40 + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, unitychan.transform.position.z + 40 + offsetZ);
                    }

                }

            }
        unitychanPosition = unitychan.transform.position.z;
        }
    }
 }
