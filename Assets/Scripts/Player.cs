using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���a
/// </summary>
public class Player : MonoBehaviour
{
    #region ��ҼҦ�
    public static Player instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region ���
    [SerializeField] Rigidbody rb = null;                       //���z�t��
    [SerializeField] public Transform cameraFollow = null;             //��v�����H���ؼ�(��������b)
    [SerializeField] Transform cameraTransform = null;          //��v�����y��
    [SerializeField] float moveSpeed = 1.5f;                    //���ʳt��
    [SerializeField] float runSpeed = 5f;                       //�]�B�t��
    [SerializeField] float mouseSpeed = 5f;                     //���Y�F�ӫ�
    [SerializeField] float jumpPower = 8f;                      //���D�O
    [SerializeField] LayerMask interactableMask;                //�i���ʪ��ϼh
    [SerializeField] Transform interactUI;                      //����UI
    [SerializeField] LayerMask seenableMask;                    //���u�i���H���ϼh(�p�g�Ӧ���v��)
    [SerializeField] IKControll ikControl = null;               //IK����
    [SerializeField] Animator kyleAnimator = null;              //����ʵe���
    [SerializeField] public Transform ��������b = null;         //����������઺�b��
    [SerializeField] public Transform eyes = null;

    float ws = 0f;
    float ad = 0f;
    float speed = 0f;
    float mouseX = 0f;
    float mouseY = 0f;
    public float mouseYTotal = 0f;
    RaycastHit aimedThing;                                      //���u�˷Ǫ��i���ʪ���
    bool aimSomething;                                          //�O�_���˷Ǩ�i���ʪ���
    RaycastHit thingInSight;                                    //���u�˷Ǫ�����
    bool sight;                                                 //�O�_���ݨ쪫��
    float cameraToPlayerDistance;                               //�q��v���쪱�a���Z��

    /// <summary>
    /// �P�ɳ]�w������D�ʵe
    /// </summary>
    bool onGround
    {
        get { return _onground; }
        set 
        { 
            _onground = value;
            kyleAnimator.SetBool("ONFLOOR", value);
        }
    }
    bool _onground = false;
    #endregion

    #region �ƥ�
    private void Start()
    {
        //���L�C����ƪ��P�w
        if (SaveManager.instance.continueGame == true)
        {
            this.transform.position = SaveManager.instance.saveData.playerPos;                      //���a����m

            ��������b.localRotation = Quaternion.Euler(0f, SaveManager.instance.saveData.playerRotate.y, 0f);
            mouseYTotal = SaveManager.instance.saveData.playerRotate.x;

            SaveManager.instance.continueGame = false;                                              //�w���J�C���N����
        }
        Cursor.lockState = CursorLockMode.Locked;       //��w�ƹ�
    }

    private void Update()
    {
        KyleAnimate();                      //����ʵe�t��
        Move();                             //����
        Interact();                         //����
    }

    /// <summary>
    /// ����lag�����z�t��
    /// </summary>
    private void FixedUpdate()
    { 
        InteractDetect();               //���ʰ���
        JumpDetect();                   //���D����
    }

    /// <summary>
    /// �̫�B�檺��s
    /// </summary>
    private void LateUpdate()
    {
        InteractUI();                   //����UI
    }
    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        //����
        ws = Mathf.Lerp(ws, Input.GetAxisRaw("Vertical"), Time.deltaTime * 10f);        //���o����(WS)��J��
        ad = Mathf.Lerp(ad, Input.GetAxisRaw("Horizontal"), Time.deltaTime * 10f);      //���o����(AD)��J��
        Vector3 move;
        move.x = ad * Mathf.Lerp(moveSpeed, runSpeed, speed);                           //�b�����P�]�B�t�׶��ϥκ��i��
        move.y = rb.velocity.y;
        move.z = ws * Mathf.Lerp(moveSpeed, runSpeed, speed);                           //�b�����P�]�B�t�׶��ϥκ��i��

        //����shift�]�B
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, 1f, Time.deltaTime * 10f);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 10f);
        }

        //�q�@�ɮy���ഫ����������b���y��
        Vector3 �ഫ�y�� = ��������b.transform.TransformVector(move);
        rb.velocity = �ഫ�y��;

        //���D
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        //����
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.timeScale;                //���Y�������C���t�ױ���
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.timeScale;
        mouseYTotal += mouseY * -1f;                                                    //�ƹ��������ʶq�|�[
        mouseYTotal = Mathf.Clamp(mouseYTotal, -80f, 80f);
        Quaternion rotateY = Quaternion.Euler(mouseYTotal, 0f, 0f);
        //�����b�����������b
        ��������b.transform.Rotate(0f, mouseX, 0f);
        //�����b���૫������b
        cameraFollow.localRotation = rotateY;
    }

    /// <summary>
    /// ���D����
    /// </summary>
    private void JumpDetect()
    {
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 1.2f);        //�q���a���ߦV�U�o�g�p�g�����a�O
    }

    /// <summary>
    /// ���ʰ���
    /// </summary>
    private void InteractDetect()
    {
        //��v���쪱�a���Z��
        cameraToPlayerDistance = Vector3.Distance(cameraTransform.position, cameraFollow.position);
        //�q��v���g�X�����쪫��
        aimSomething = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out aimedThing, cameraToPlayerDistance + 2f, interactableMask);

        //IK����:����ݦV�Ǥߺ˷ǳB
        sight = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out thingInSight, 999f, seenableMask);
        if(sight)
        {
            ikControl.lookAt = thingInSight.point;
        }
        else
        {
            ikControl.lookAt = cameraTransform.TransformPoint(0f, 0f, 1000f);
        }
    }

    /// <summary>
    /// ����UI���}��
    /// </summary>
    private void InteractUI()
    {
        if (aimSomething) interactUI.localScale = Vector3.one;
        else interactUI.localScale = Vector3.zero;
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && aimSomething && !��ܨt��.instance.isPlay)
        {
            aimedThing.collider.transform.root.GetComponent<Interactable>().Interact();
        }
    }

    /// <summary>
    /// ���ʰʵe
    /// </summary>
    private void KyleAnimate()
    {
        kyleAnimator.SetFloat("WS", ws);
        kyleAnimator.SetFloat("AD", ad);
        kyleAnimator.SetFloat("SPEED", speed);
    }
    #endregion
}
