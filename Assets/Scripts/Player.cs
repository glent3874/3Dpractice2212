using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    private void Awake()
    {
        instance = this;
    }

    #region ���
    [SerializeField] Rigidbody rb = null;
    [SerializeField] Transform cameraFollow = null;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float runSpeed = 5f; 
    [SerializeField] float mouseSpeed = 5f;
    [SerializeField] float jumpPower = 8f;
    [SerializeField] LayerMask interactableMask;
    [SerializeField] Transform interactUI;
    [SerializeField] LayerMask seenableMask;
    [SerializeField] IKControll ikControl = null;
    [SerializeField] Animator kyleAnimator = null;
    [SerializeField] Transform ��������b = null;

    float ws = 0f;
    float ad = 0f;
    float speed = 0f;
    float mouseX = 0f;
    float mouseY = 0f;
    float mouseYTotal = 0f;
    RaycastHit aimedThing;
    bool aimSomething;
    //���u�ݨ쪺�F��
    RaycastHit thingInSight;
    bool sight;
    //�q��v���쪱�a���Z��
    float cameraToPlayerDistance;
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        KyleAnimate();
        Move();
        Interact();
    }

    /// <summary>
    /// ����lag�����z�t��
    /// </summary>
    private void FixedUpdate()
    { 
        InteractDetect();
        JumpDetect();
    }

    /// <summary>
    /// UI
    /// </summary>
    private void LateUpdate()
    {
        InteractUI();
    }
    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        //����
        ws = Mathf.Lerp(ws, Input.GetAxisRaw("Vertical"), Time.deltaTime * 10f);
        ad = Mathf.Lerp(ad, Input.GetAxisRaw("Horizontal"), Time.deltaTime * 10f);
        Vector3 move = new Vector3(ad * moveSpeed, rb.velocity.y, ws * moveSpeed);
        move.x = ad * Mathf.Lerp(moveSpeed, runSpeed, speed);
        move.y = rb.velocity.y;
        move.z = ws * Mathf.Lerp(moveSpeed, runSpeed, speed);

        //�]�B
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
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseYTotal += mouseY * -1f;
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
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 0.9f);
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
            aimedThing.collider.transform.root.GetComponent<Item>().interact();
        }
    }

    private void KyleAnimate()
    {
        kyleAnimator.SetFloat("WS", ws);
        kyleAnimator.SetFloat("AD", ad);
        kyleAnimator.SetFloat("SPEED", speed);
    }
    #endregion
}
