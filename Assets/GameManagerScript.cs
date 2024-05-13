using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //追加
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    int[,] map;　//レベルデザイン用の配列
    GameObject[,] field;//ゲーム管理用の配列

    // Start is called before the first frame update

    /*void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }*/

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            //nullチェック
            {
                if (field[y, x] == null) { continue; }
                //player
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    //再帰処理(箱を押す)　p43
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        //移動先に2(箱)が居たら
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            //どの方向へ移動するかを算出
            Vector2Int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先へ2(箱)を移動させる
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
       

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x].transform.position =
            new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }

    void Start()
    {
        /*//追加
        GameObject instance = Instantiate(playerPrefab,
            new Vector3(0, 0, 0), Quaternion.identity);*/
        //mapの生成
        map = new int[,]
        {
            {1,0,2,0,0},
            {0,0,2,0,0},
            {0,0,2,0,0}
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
                        new Vector3(x, map.GetLength(0) - y, 0.0f),
                        Quaternion.identity
                        );
                }
                if (map[y, x] == 2)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0.0f),
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
            //移動処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
            //PrintArray();
        }

        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //移動処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
            //PrintArray();
        }

        //上移動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //移動処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
            //PrintArray();
        }

        //下移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            //PrintArray();
        }
    }

}
