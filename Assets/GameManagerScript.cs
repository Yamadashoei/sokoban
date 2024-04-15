using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[] map;
    // Start is called before the first frame update
    void Start()
    {

        map = new int[] { 0, 0, 0, 1, 1 };
        string debugText = "";
        //Debug.Log("Hello World");
        for (int i = 0; i < map.Length; i++)
        {
            //Debug.Log(map[i] + ",");
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //ˆÚ“®ˆ—
            int playerIndex = -1;
            //—v‘f”‚Ímap.Length‚ÅŽæ“¾
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }



        }
    }
}
