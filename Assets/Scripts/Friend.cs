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
    #endregion

    #region Sign
    private void Awake()
    {
        AddStatus(FriendBehaviour.Idle, StartIdle, Idleing, EndIdle);
        AddStatus(FriendBehaviour.Walk, StartWalk, Walking, EndWalk);
        AddStatus(FriendBehaviour.Talk, StartTalk, Talking, EndTalking);
        allAIPoint = GameObject.FindGameObjectsWithTag("AIPoint");
        導航器.updatePosition = false;
        導航器.updateRotation = false;
        導航器.updateUpAxis = false;
    }
    #endregion

    #region Idle
    void StartIdle()
    {
        idleTime = Random.Range(0.5f, 3f);
    }
    void Idleing()
    {
        if (statusTime > idleTime)
        {
            status = FriendBehaviour.Walk;
        }
    }
    void EndIdle()
    {

    }
    #endregion

    #region Walk
    void StartWalk()
    {
        walkTime = Random.Range(10f, 20f);
        walkTarget = allAIPoint[Random.Range(0, allAIPoint.Length)].transform.position;
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

    #region Talk
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

    #region Support
    void LookAt(Vector3 position, float speed = 10f)
    {
        position.y = this.transform.position.y;
        Quaternion ARotation = this.transform.rotation;
        Quaternion BRotation = Quaternion.LookRotation(position - this.transform.position, new Vector3(0f, 1f, 0));
        Quaternion result = Quaternion.Lerp(ARotation, BRotation, Time.deltaTime * speed);
        this.transform.rotation = result;
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

    #region Else
    protected override void Update()
    {
        base.Update();
        導航器.nextPosition = this.transform.position;
    }
    public void interact()
    {
        status = FriendBehaviour.Talk;
    }
    #endregion
}

public enum FriendBehaviour
{
    Idle = 0, 
    Walk = 1,
    Talk = 2,
}
