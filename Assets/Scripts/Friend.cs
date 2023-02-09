using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Friend : AYEStatusBehaviour<FriendBehaviour>, Item
{
    #region ���
    [SerializeField] Animator friendAnimator = null;
    [SerializeField] NavMeshAgent �ɯ边 = null;
    [SerializeField] float talkDistance = 1f;
    [SerializeField] NpcData �Ĥ@����� = null;

    GameObject[] allAIPoint = new GameObject[0];
    Vector3 walkTarget;
    Vector3 �n�ݭ��� = Vector3.zero;
    float �n���n�� = 0f;
    float idleTime = 0f;
    float walkTime = 0f;
    bool �O�_�b���� = false;
    int ���ʦ��� = 0;
    #endregion

    #region �n�O���A ��l��    
    private void Awake()
    {
        AddStatus(FriendBehaviour.Idle, StartIdle, Idleing, EndIdle);
        AddStatus(FriendBehaviour.Walk, StartWalk, Walking, EndWalk);
        AddStatus(FriendBehaviour.Talk, StartTalk, Talking, EndTalking);
        allAIPoint = GameObject.FindGameObjectsWithTag("AIPoint");          //����Ҧ��ؼ��I
        �ɯ边.updatePosition = false;
        �ɯ边.updateRotation = false;
        �ɯ边.updateUpAxis = false;
    }
    #endregion

    #region ���m
    void StartIdle()
    {
        idleTime = Random.Range(0.5f, 3f);      //�H���o�b�ɶ�
    }
    void Idleing()
    {
        if (statusTime > idleTime)              //statusTime �ŧi�󩳼h�� �����A������ɶ�
        {
            status = FriendBehaviour.Walk;
        }
    }
    void EndIdle()
    {

    }
    #endregion

    #region ����
    void StartWalk()
    {
        walkTime = Random.Range(10f, 20f);
        walkTarget = allAIPoint[Random.Range(0, allAIPoint.Length)].transform.position;     //�H����ܤ@�ӥؼ��I
        friendAnimator.SetBool("Walk", true);
    }
    void Walking()
    {
        Vector3 cornor = �ɯ�(walkTarget);
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

    #region ���
    void StartTalk()
    {
        �n���n�� = 1f;
    }
    void Talking()
    {
        �n�ݭ��� = Player.instance.eyes.position;
        Vector3 cornor = �ɯ�(Player.instance.transform.position);
        if (PlayerDistance() > talkDistance)
        {
            LookAt(cornor);
            friendAnimator.SetBool("Walk", true);
        }
        else
        {
            friendAnimator.SetBool("Walk", false);
            if(�O�_�b���� == false)
            {
                �O�_�b���� = true;
                ��ܨt��.instance.��ܵ����n�e�����Ʊ� += �����ܤF;
                ��ܨt��.instance.�}�l���(�Ĥ@�����);
            }
        }
    }
    void EndTalking()
    {
        �n���n�� = 0f;
    }
    void �����ܤF()
    {
        ��ܨt��.instance.��ܵ����n�e�����Ʊ� -= �����ܤF;
        status = FriendBehaviour.Walk;
    }
    #endregion

    #region �䴩
    void LookAt(Vector3 position, float speed = 10f)
    {
        position.y = this.transform.position.y;                                                                         //�N�ؼЦ�m�����׳]����ۤv�@��
        Quaternion ARotation = this.transform.rotation;                                                                 //�ۤv�{�b����V
        Quaternion BRotation = Quaternion.LookRotation(position - this.transform.position, new Vector3(0f, 1f, 0));     //�ؼФ�V
        Quaternion result = Quaternion.Lerp(ARotation, BRotation, Time.deltaTime * speed);                              //�v������
        this.transform.rotation = result;                                                                               //���G
    }
    bool Nearby(Vector3 position, float �{�ɭ� = 0.2f)
    {
        position.y = this.transform.position.y;
        float distance = Vector3.Distance(this.transform.position, position);
        return distance < �{�ɭ�;
    }
    Vector3 �ɯ�(Vector3 position)
    {
        �ɯ边.SetDestination(position);
        return �ɯ边.steeringTarget;
    }
    float PlayerDistance()  
    {
        return Vector3.Distance(this.transform.position, Player.instance.transform.position);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        friendAnimator.SetLookAtPosition(�n�ݭ���);
        friendAnimator.SetLookAtWeight(�n���n��);
    }
    #endregion

    #region ��L
    protected override void Update()
    {
        base.Update();
        �ɯ边.nextPosition = this.transform.position;
    }   
    public void Interact()
    {
        if(���ʦ��� == 0)
        {
            status = FriendBehaviour.Talk;
            ���ʦ���++;
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
