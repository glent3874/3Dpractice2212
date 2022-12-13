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
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float mouseSpeed = 5f;
    [SerializeField] float jumpPower = 8f;
    [SerializeField] LayerMask interactableMask;
    [SerializeField] Transform interactUI;
    [SerializeField] Animator kyleAnimator = null;

    float ws = 0f;
    float ad = 0f;
    RaycastHit aimedThing;
    bool aimSomething;
    bool onGround;
    //�q��v���쪱�a���Z��
    float cameraToPlayerDistance;
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

        //�q�@�ɮy���ഫ�������󪺮y��
        Vector3 �ഫ�y�� = this.transform.TransformVector(move);
        rb.velocity = �ഫ�y��;

        //���D
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        //����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //�����b���হ����
        this.transform.Rotate(0f, mouseX * mouseSpeed, 0f);
        //�����b���૫������b
        cameraFollow.Rotate(-mouseY * mouseSpeed, 0f, 0f);
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
    }
    #endregion
}
