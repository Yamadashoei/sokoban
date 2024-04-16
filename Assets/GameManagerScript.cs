using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[] map;
    // Start is called before the first frame update

    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    //再帰処理(箱を押す)
    bool MoveNumber(
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

    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }


    void Start()
    {
        map = new int[] { 0, 0, 2, 0, 1, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //移動処理
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }


        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //移動処理
            int playerIndex = GetPlayerIndex();
            /*//要素数はmap.Lengthで取得
            if (playerIndex < map.Length - 1)
            {
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
            }*/
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }

}
