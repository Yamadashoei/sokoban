using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //追加
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab; //チャレンジ課題ゴール
    public GameObject particlePrefab; // パーティクルプレハブ
    public GameObject wallPrefab; // 壁プレハブ

    public GameObject clearText;
    int[,] map;　//レベルデザイン用の配列
    GameObject[,] field;//ゲーム管理用の配列
    int[,] firstMap; // 初期状態のマップを保存するための配列

    // Start is called before the first frame update

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
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        // 移動先に壁がある場合
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Wall")
        {
            return false;
        }

        // 移動先に箱がある場合
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            // どの方向へ移動するかを算出
            Vector2Int velocity = moveTo - moveFrom;
            // プレイヤーの移動先からさらに先へ箱を移動させる
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        // プレイヤーの移動時にパーティクルを生成
        if (field[moveFrom.y, moveFrom.x].tag == "Player")
        {
            for (int particle = 0; particle < 5; particle++)
            {
                Instantiate(
                    particlePrefab,
                    new Vector3(moveFrom.x, map.GetLength(0) - moveFrom.y, 0.0f),
                    Quaternion.identity
                );
            }
        }

        // プレイヤーを新しい位置に移動
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x].GetComponent<Move>().MoveTo(moveToPosition);

        // 元の位置を空にする
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }

    bool IsCleard()
    {
        //Vector2Int型の可変長列の作成
        List<Vector2Int> goals = new List<Vector2Int>();
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //格納場所かどうかを判断
                if (map[y, x] == 3)
                {
                    //格納場所のインデックスを控える
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //一つでも箱が無かったら条件未達成
                return false;
            }
        }
        //条件未達成で無ければ条件達成
        return true;
    }

    void Start()
    {
        //mapの生成
        map = new int[,]
        {
            {4,4,4,4,4,4,4,4,4,4,4,4},
            {4,1,0,0,0,0,0,0,0,0,3,4},
            {4,0,0,0,2,0,0,0,0,0,0,4},
            {4,0,0,2,0,2,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,4},
            {4,3,0,0,0,0,0,0,0,0,3,4},
            {4,4,4,4,4,4,4,4,4,4,4,4}
        };

        // 初期状態のマップを保存
        firstMap = (int[,])map.Clone();

        InitializeField();
    }

    void InitializeField()
    {
        Vector3 cameraPos = new Vector3(map.GetLength(1) / 2, map.GetLength(0) / 2 + 1, -10);
        Camera.main.transform.position = cameraPos;

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
                if (map[y, x] == 3)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        goalPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0.01f),
                        Quaternion.identity
                        );
                }
                if (map[y, x] == 4)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        wallPrefab,
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
    }

    void ReStartGame()
    {
        // 既存のフィールドをクリア x1,y0
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] != null)
                {
                    Destroy(field[y, x]);
                }
            }
        }

        // mapを初期状態に戻す
        map = (int[,])firstMap.Clone();

        // フィールドを初期化
        InitializeField();

        // クリアテキストを非表示にする
        clearText.SetActive(false);
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
            //もしクリアしたら
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //移動処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
            //もしクリアしたら
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        //上移動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //移動処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
            //もしクリアしたら
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        //下移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            //もしクリアしたら
            if (IsCleard())
            {
                clearText.SetActive(true);
            }
        }

        //リセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReStartGame();
        }
    }
}
