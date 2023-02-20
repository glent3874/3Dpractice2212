using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 2F ��NPC
/// </summary>
public class Friend : AYEStatusBehaviour<FriendBehaviour>, Interactable
{
    #region ���
    [SerializeField] Animator friendAnimator = null;
    [SerializeField] NavMeshAgent �ɯ边 = null;
    [SerializeField] float talkDistance = 1f;
    [SerializeField] NpcData �Ĥ@����� = null;
    [SerializeField] NpcData �ĤG����� = null;
    [SerializeField] NpcData �ĤT����� = null;

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
        Vector3 cornor = �ɯ�(walkTarget);                   //���|�`�I
        LookAt(cornor);                                     //�ݬۭn�e�������|�`�I
        // �{�b�����A�ɶ��W�L�����ɶ� �� �a��ؼ��I��
        // �ܦ����m���A
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
        �n�ݭ��� = Player.instance.eyes.position;                       //�ݦV���a������m
        Vector3 cornor = �ɯ�(Player.instance.transform.position);     //���ܮɾɦV���a��m
        //�p�G���a��m�L�� �W�X��ܶZ��
        //�^�쨵�޼Ҧ�
        //�S���W�X
        //�i�J���
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
                ��ܨt��.instance.��ܵ����n�e�����Ʊ� += �����ܤF;       //�e������ܨt�� ��ܵ����� ���� �����ܤF

                if(���ʦ��� == 0)
                {
                    ��ܨt��.instance.�}�l���(�Ĥ@�����);
                }
                else if (���ʦ��� == 1)
                {
                    ��ܨt��.instance.�}�l���(�ĤG�����);
                }
                else if (���ʦ��� == 2)
                {
                    ��ܨt��.instance.�}�l���(�ĤT�����);
                }
                ���ʦ���++;
                
            }
        }
    }
    void EndTalking()
    {
        �O�_�b���� = false;
        �n���n�� = 0f;
    }

    /// <summary>
    /// ��ܧ������q�\�e��
    /// </summary>
    void �����ܤF()
    {
        �O�_�b���� = false;
        ��ܨt��.instance.��ܵ����n�e�����Ʊ� -= �����ܤF;
        status = FriendBehaviour.Walk;
    }
    #endregion

    #region �䴩
    /// <summary>
    /// �v������ܥؼФ�V �������V���ĪG
    /// �ϥα���q�B�z
    /// </summary>
    /// <param name="position"></param>
    /// <param name="speed"></param>
    void LookAt(Vector3 position, float speed = 10f)
    {
        position.y = this.transform.position.y;                                                                         //�N�ؼЦ�m�����׳]����ۤv�@��
        Quaternion ARotation = this.transform.rotation;                                                                 //�ۤv�{�b����V
        Quaternion BRotation = Quaternion.LookRotation(position - this.transform.position, new Vector3(0f, 1f, 0));     //�ؼФ�V
        Quaternion result = Quaternion.Lerp(ARotation, BRotation, Time.deltaTime * speed);                              //�v������
        this.transform.rotation = result;                                                                               //���G
    }

    /// <summary>
    /// ����NPC��ؼЦ�m���Z��
    /// </summary>
    /// <param name="position">�ؼЦ�m</param>
    /// <param name="�{�ɭ�">�i����������Z��</param>
    /// <returns></returns>
    bool Nearby(Vector3 position, float �{�ɭ� = 0.2f)
    {
        position.y = this.transform.position.y;
        float distance = Vector3.Distance(this.transform.position, position);
        return distance < �{�ɭ�;
    }

    /// <summary>
    /// �]�m�ت��a �æ^�ǭn���s���I
    /// </summary>
    /// <param name="position">�n�h���a��</param>
    /// <returns>���|�`�I</returns>
    Vector3 �ɯ�(Vector3 position)
    {
        �ɯ边.SetDestination(position);           //�]�w�ت��a
        return �ɯ边.steeringTarget;
    }

    /// <summary>
    /// ���a�P��NPC���Z��
    /// </summary>
    /// <returns></returns>
    float PlayerDistance()  
    {
        return Vector3.Distance(this.transform.position, Player.instance.transform.position);
    }

    /// <summary>
    /// �������ݦV��m IK����
    /// </summary>
    /// <param name="layerIndex">��ݱo�ϼh</param>
    private void OnAnimatorIK(int layerIndex)
    {
        friendAnimator.SetLookAtPosition(�n�ݭ���); //�ݪ���m
        friendAnimator.SetLookAtWeight(�n���n��);   //�ݪ��v�� �]�m�C�|���n�ݤ���(�ײ�)���ĪG
    }
    #endregion

    #region ��L
    protected override void Update()
    {
        base.Update();
        �ɯ边.nextPosition = this.transform.position;
    }

    /// <summary>
    /// ���� �T���H��������
    /// </summary>
    public void Interact()
    {
        if(���ʦ��� <3)
            status = FriendBehaviour.Talk;
    }
    #endregion
}

#region ���A
/// <summary>
/// NPC�����A
/// </summary>
public enum FriendBehaviour
{
    Idle = 0, 
    Walk = 1,
    Talk = 2,
}
#endregion