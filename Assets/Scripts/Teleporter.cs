using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 傳送門
/// </summary>
public class Teleporter : MonoBehaviour
{
    #region 欄位
    [SerializeField] Transform teleportTarget;  //傳送目的地
    [SerializeField] GameObject player;         //玩家
    [SerializeField] GameObject 水平旋轉軸;      //傳送後使玩家面向門的前方 旋轉的目標物
    [SerializeField] Transform lookAt;          //玩家看向的點
    #endregion

    #region 方法
    /// <summary>
    /// 接觸到門之後
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //如果接觸物的標籤為"玩家"
        if(other.CompareTag("Player"))
        {
            player.transform.position = teleportTarget.position;                                                        //將玩家傳送至目的地
            lookAt.position = lookAt.position + new Vector3(0f, player.transform.position.y - lookAt.position.y, 0f);   //使玩家看向的點的高度y等於玩家的高度y
            水平旋轉軸.transform.LookAt(lookAt);                                                                         //使玩家面向看向的點
        }
    }
    #endregion
}
