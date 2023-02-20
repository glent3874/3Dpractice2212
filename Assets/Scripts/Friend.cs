using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 2F 的NPC
/// </summary>
public class Friend : AYEStatusBehaviour<FriendBehaviour>, Interactable
{
    #region 欄位
    [SerializeField] Animator friendAnimator = null;
    [SerializeField] NavMeshAgent 導航器 = null;
    [SerializeField] float talkDistance = 1f;
    [SerializeField] NpcData 第一次對話 = null;
    [SerializeField] NpcData 第二次對話 = null;
    [SerializeField] NpcData 第三次對話 = null;

    GameObject[] allAIPoint = new GameObject[0];
    Vector3 walkTarget;
    Vector3 要看哪裡 = Vector3.zero;
    float 要不要看 = 0f;
    float idleTime = 0f;
    float walkTime = 0f;
    bool 是否在講話 = false;
    int 互動次數 = 0;
    #endregion

    #region 登記狀態 初始化    
    private void Awake()
    {
        AddStatus(FriendBehaviour.Idle, StartIdle, Idleing, EndIdle);
        AddStatus(FriendBehaviour.Walk, StartWalk, Walking, EndWalk);
        AddStatus(FriendBehaviour.Talk, StartTalk, Talking, EndTalking);
        allAIPoint = GameObject.FindGameObjectsWithTag("AIPoint");          //獲取所有目標點
        導航器.updatePosition = false;
        導航器.updateRotation = false;
        導航器.updateUpAxis = false;
    }
    #endregion

    #region 閒置
    void StartIdle()
    {
        idleTime = Random.Range(0.5f, 3f);      //隨機發呆時間
    }
    void Idleing()
    {
        if (statusTime > idleTime)              //statusTime 宣告於底層中 指狀態的持續時間
        {
            status = FriendBehaviour.Walk;
        }
    }
    void EndIdle()
    {
        
    }
    #endregion

    #region 走路
    void StartWalk()
    {
        walkTime = Random.Range(10f, 20f);
        walkTarget = allAIPoint[Random.Range(0, allAIPoint.Length)].transform.position;     //隨機選擇一個目標點
        friendAnimator.SetBool("Walk", true);
    }
    void Walking()
    {
        Vector3 cornor = 導航(walkTarget);                   //路徑節點
        LookAt(cornor);                                     //看相要前往的路徑節點
        // 現在的狀態時間超過走路時間 或 靠近目標點時
        // 變成閒置狀態
        if (statusTime > walkTime || Nearby(walkTarget))
        {
            status = FriendBehaviour.Idle;
        }
    }
    void EndWalk()
    {
        friendAnimator.SetBool("Walk", false);
    }
    #endregion

    #region 對話
    void StartTalk()
    {
        要不要看 = 1f;
    }
    void Talking()
    {
        要看哪裡 = Player.instance.eyes.position;                       //看向玩家眼睛位置
        Vector3 cornor = 導航(Player.instance.transform.position);     //講話時導向玩家位置
        //如果玩家位置過遠 超出對話距離
        //回到巡邏模式
        //沒有超出
        //進入對話
        if (PlayerDistance() > talkDistance)
        {
            LookAt(cornor);
            friendAnimator.SetBool("Walk", true);
        }
        else
        {
            friendAnimator.SetBool("Walk", false);
            if(是否在講話 == false)
            {
                是否在講話 = true;
                對話系統.instance.對話結束要委派的事情 += 講完話了;       //委派給對話系統 對話結束時 執行 講完話了

                if(互動次數 == 0)
                {
                    對話系統.instance.開始對話(第一次對話);
                }
                else if (互動次數 == 1)
                {
                    對話系統.instance.開始對話(第二次對話);
                }
                else if (互動次數 == 2)
                {
                    對話系統.instance.開始對話(第三次對話);
                }
                互動次數++;
                
            }
        }
    }
    void EndTalking()
    {
        是否在講話 = false;
        要不要看 = 0f;
    }

    /// <summary>
    /// 對話完取消訂閱委派
    /// </summary>
    void 講完話了()
    {
        是否在講話 = false;
        對話系統.instance.對話結束要委派的事情 -= 講完話了;
        status = FriendBehaviour.Walk;
    }
    #endregion

    #region 支援
    /// <summary>
    /// 逐漸旋轉至目標方向 做成面向的效果
    /// 使用旋轉量處理
    /// </summary>
    /// <param name="position"></param>
    /// <param name="speed"></param>
    void LookAt(Vector3 position, float speed = 10f)
    {
        position.y = this.transform.position.y;                                                                         //將目標位置的高度設為跟自己一樣
        Quaternion ARotation = this.transform.rotation;                                                                 //自己現在的方向
        Quaternion BRotation = Quaternion.LookRotation(position - this.transform.position, new Vector3(0f, 1f, 0));     //目標方向
        Quaternion result = Quaternion.Lerp(ARotation, BRotation, Time.deltaTime * speed);                              //逐漸旋轉
        this.transform.rotation = result;                                                                               //結果
    }

    /// <summary>
    /// 偵測NPC跟目標位置的距離
    /// </summary>
    /// <param name="position">目標位置</param>
    /// <param name="臨界值">可接受的接近距離</param>
    /// <returns></returns>
    bool Nearby(Vector3 position, float 臨界值 = 0.2f)
    {
        position.y = this.transform.position.y;
        float distance = Vector3.Distance(this.transform.position, position);
        return distance < 臨界值;
    }

    /// <summary>
    /// 設置目的地 並回傳要轉彎的點
    /// </summary>
    /// <param name="position">要去的地方</param>
    /// <returns>路徑節點</returns>
    Vector3 導航(Vector3 position)
    {
        導航器.SetDestination(position);           //設定目的地
        return 導航器.steeringTarget;
    }

    /// <summary>
    /// 玩家與此NPC的距離
    /// </summary>
    /// <returns></returns>
    float PlayerDistance()  
    {
        return Vector3.Distance(this.transform.position, Player.instance.transform.position);
    }

    /// <summary>
    /// 眼睛的看向位置 IK控制
    /// </summary>
    /// <param name="layerIndex">能看得圖層</param>
    private void OnAnimatorIK(int layerIndex)
    {
        friendAnimator.SetLookAtPosition(要看哪裡); //看的位置
        friendAnimator.SetLookAtWeight(要不要看);   //看的權重 設置低會有要看不看(斜眼)的效果
    }
    #endregion

    #region 其他
    protected override void Update()
    {
        base.Update();
        導航器.nextPosition = this.transform.position;
    }

    /// <summary>
    /// 互動 三次以內都能對話
    /// </summary>
    public void Interact()
    {
        if(互動次數 <3)
            status = FriendBehaviour.Talk;
    }
    #endregion
}

#region 狀態
/// <summary>
/// NPC的狀態
/// </summary>
public enum FriendBehaviour
{
    Idle = 0, 
    Walk = 1,
    Talk = 2,
}
#endregion