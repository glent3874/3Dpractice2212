using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Friend : AYEStatusBehaviour<FriendBehaviour>, Item
{
    #region 欄位
    [SerializeField] Animator friendAnimator = null;
    [SerializeField] NavMeshAgent 導航器 = null;
    [SerializeField] float talkDistance = 1f;
    [SerializeField] NpcData 第一次對話 = null;

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
        Vector3 cornor = 導航(walkTarget);
        LookAt(cornor);
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
        要看哪裡 = Player.instance.eyes.position;
        Vector3 cornor = 導航(Player.instance.transform.position);
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
                對話系統.instance.對話結束要委派的事情 += 講完話了;
                對話系統.instance.開始對話(第一次對話);
            }
        }
    }
    void EndTalking()
    {
        要不要看 = 0f;
    }
    void 講完話了()
    {
        對話系統.instance.對話結束要委派的事情 -= 講完話了;
        status = FriendBehaviour.Walk;
    }
    #endregion

    #region 支援
    void LookAt(Vector3 position, float speed = 10f)
    {
        position.y = this.transform.position.y;                                                                         //將目標位置的高度設為跟自己一樣
        Quaternion ARotation = this.transform.rotation;                                                                 //自己現在的方向
        Quaternion BRotation = Quaternion.LookRotation(position - this.transform.position, new Vector3(0f, 1f, 0));     //目標方向
        Quaternion result = Quaternion.Lerp(ARotation, BRotation, Time.deltaTime * speed);                              //逐漸旋轉
        this.transform.rotation = result;                                                                               //結果
    }
    bool Nearby(Vector3 position, float 臨界值 = 0.2f)
    {
        position.y = this.transform.position.y;
        float distance = Vector3.Distance(this.transform.position, position);
        return distance < 臨界值;
    }
    Vector3 導航(Vector3 position)
    {
        導航器.SetDestination(position);
        return 導航器.steeringTarget;
    }
    float PlayerDistance()  
    {
        return Vector3.Distance(this.transform.position, Player.instance.transform.position);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        friendAnimator.SetLookAtPosition(要看哪裡);
        friendAnimator.SetLookAtWeight(要不要看);
    }
    #endregion

    #region 其他
    protected override void Update()
    {
        base.Update();
        導航器.nextPosition = this.transform.position;
    }   
    public void Interact()
    {
        if(互動次數 == 0)
        {
            status = FriendBehaviour.Talk;
            互動次數++;
        }
    }
    #endregion
}

public enum FriendBehaviour
{
    Idle = 0, 
    Walk = 1,
    Talk = 2,
}
