using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class GameManegerScript : MonoBehaviour
{


    //追加
    public GameObject PlayerPrefab;
    int[,] map;
    GameObject[,] field;//ゲーム管理用の配列


    void Start()
    {
        //GameObject instance = Instantiate(
        //    PlayerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity
        //
        //    );

        map = new int[,]
        {
          {1,0,0,0,0 },
          {0,0,0,0,0 },
          {0,0,0,0,0 },
        };

        string debugTXT = "";

        //変更。二重for文で二重配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        PlayerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }

                //debugTXT += map[x, y].ToString() + ",";
            }
            //debugTXT += "\n";//改行

        }

        Debug.Log(debugTXT);

        //PrintArray();
    }


    //private void PrintArray()
    //{
    //    string debugtext = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        debugtext += map[i].ToString() + ",";
    //    }
    //    Debug.Log(debugtext);
    //}


    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player") { return new Vector2Int(x, y); }
            }
        }
        return new Vector2Int(-1, -1);
    }

    //nullチェックをしてからタグのチェックを行う



    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {


        //縦軸横軸の配列外参照をしていないか
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        //Boxタグを持っていたら再起処理
        if (field[moveTo.x, moveTo.y] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        //GameObjectの座標(position)を稼働させてからインデックスを入れ替え

        field[moveFrom.y, moveFrom.x].transform.position =
            new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];

        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);

        field[moveFrom.y, moveFrom.x] = null;

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        return true;





    }


}

//int[] map = { 0, 0, 0, 2, 0, 1, 0, 2, 0, 0, 0 };






// Start is called before the first frame update
//void Start()
//{
//
//   // PrintArray();
//}

// Update is called once per frame
//void Update()
//{

//if (Input.GetKeyDown(KeyCode.RightArrow))
//{
//    int PlayerIndex = GetPlayerIndex();
//
//    PrintArray();
//
//    MoveNumber(1, PlayerIndex, PlayerIndex + 1);
//}
//
//
//if (Input.GetKeyDown(KeyCode.LeftArrow))
//{
//    int PlayerIndex = GetPlayerIndex();
//
//    PrintArray();
//    MoveNumber(1, PlayerIndex, PlayerIndex - 1);
//
//}
//}
