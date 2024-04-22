using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //追加
    public GameObject playerPrefab;
    int[,] map;　//レベルデザイン用の配列
    GameObject[,] field;//ゲーム管理用の配列

    // Start is called before the first frame update

    void PrintArray()
    {
        /*string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);*/
    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //nullチェック
                if (field[y, x] == null) { continue; }
                //player
                if (field[y, x].tag == "player")
                {
                    return new Vector2Int(x, y);
                }

            }
        }
        return new Vector2Int(-1, -1);
    }

    //再帰処理(箱を押す)　p43
    /*bool MoveNumber(
        int number, int moveFrom, int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.Length)
        { return false; }
        //移動先に2(箱)が居たら
        if (map[moveTo] == 2)
        {
            //どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先へ2(箱)を移動させる

            bool success =
                MoveNumber(2, moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;

    }*/

    void Start()
    {
        /*//追加
        GameObject instance = Instantiate(playerPrefab,
            new Vector3(0, 0, 0), Quaternion.identity);*/
        //mapの生成
        map = new int[,]
        {
            {1,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };

        //フィールドサイズの決定
        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        //mapに応じて描画
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - 1 - y, 0.0f),
                        Quaternion.identity
                        );
                }

            }
        }
        string debugText = "";
        //変更。二重for文で二次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";//改行
        }
        Debug.Log(debugText);
        /*map = new int[] { 0, 0, 2, 0, 1, 0 };
        PrintArray();*/
    }

    // Update is called once per frame
    void Update()
    {
        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            /* //移動処理
             int playerIndex = GetPlayerIndex();
             MoveNumber(1, playerIndex, playerIndex + 1);
             PrintArray();*/
        }


        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            /*//移動処理
             int playerIndex = GetPlayerIndex();
             //要素数はmap.Lengthで取得
             if (playerIndex < map.Length - 1)
             {
                 map[playerIndex + 1] = 1;
                 map[playerIndex] = 0;
             }
             MoveNumber(1, playerIndex, playerIndex - 1);
             PrintArray();*/
        }
    }

}
