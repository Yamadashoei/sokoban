using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�ǉ�
    public GameObject playerPrefab;
    int[,] map;�@//���x���f�U�C���p�̔z��
    GameObject[,] field;//�Q�[���Ǘ��p�̔z��

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
                //null�`�F�b�N
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

    //�ċA����(��������)�@p43
    /*bool MoveNumber(
        int number, int moveFrom, int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo < 0 || moveTo >= map.Length)
        { return false; }
        //�ړ����2(��)��������
        if (map[moveTo] == 2)
        {
            //�ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������

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
        /*//�ǉ�
        GameObject instance = Instantiate(playerPrefab,
            new Vector3(0, 0, 0), Quaternion.identity);*/
        //map�̐���
        map = new int[,]
        {
            {1,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        };

        //�t�B�[���h�T�C�Y�̌���
        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        //map�ɉ����ĕ`��
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
        //�ύX�B��dfor���œ񎟌��z��̏����o��
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";//���s
        }
        Debug.Log(debugText);
        /*map = new int[] { 0, 0, 2, 0, 1, 0 };
        PrintArray();*/
    }

    // Update is called once per frame
    void Update()
    {
        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            /* //�ړ�����
             int playerIndex = GetPlayerIndex();
             MoveNumber(1, playerIndex, playerIndex + 1);
             PrintArray();*/
        }


        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            /*//�ړ�����
             int playerIndex = GetPlayerIndex();
             //�v�f����map.Length�Ŏ擾
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
