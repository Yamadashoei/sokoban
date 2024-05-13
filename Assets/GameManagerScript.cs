using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�ǉ�
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    int[,] map;�@//���x���f�U�C���p�̔z��
    GameObject[,] field;//�Q�[���Ǘ��p�̔z��

    // Start is called before the first frame update

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            //null�`�F�b�N
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

    //�ċA����(��������)�@p43
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        //�ړ����2(��)��������
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            //�ǂ̕����ֈړ����邩���Z�o
            Vector2Int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x].transform.position =
            new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }

    bool IsCleard()
    {
        //Vector2Int�^�̉ϒ���̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���ǂ����𔻒f
                if (map[y, x] == 3)
                {
                    //�i�[�ꏊ�̃C���f�b�N�X���T����
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //��ł���������������������B��
                return false;
            }
        }
        //�������B���Ŗ�����Ώ����B��
        return true;
    }

    void Start()
    {
        //map�̐���
        map = new int[,]
        {
            {0,0,0,0,0},
            {0,3,1,3,0},
            {0,0,2,0,0},
            {0,2,0,2,0},
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
    }

    // Update is called once per frame
    void Update()
    {
        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //�ړ�����
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
            //�����N���A������
            if (IsCleard())
            {
                Debug.Log("Clear");
            }
        }

        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //�ړ�����
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
            //�����N���A������
            if (IsCleard())
            {
                Debug.Log("Clear");
            }
        }

        //��ړ�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //�ړ�����
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
            //�����N���A������
            if (IsCleard())
            {
                Debug.Log("Clear");
            }
        }

        //���ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            //�����N���A������
            if (IsCleard())
            {
                Debug.Log("Clear");
            }
        }

    }

}
