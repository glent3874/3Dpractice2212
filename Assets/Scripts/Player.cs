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
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float mouseSpeed = 5f;
    [SerializeField] float jumpPower = 8f;
    [SerializeField] LayerMask interactableMask;
    [SerializeField] Transform interactUI;

    RaycastHit aimedThing;
    bool aimSomething;
    bool onGround;
    #endregion

    #region �ƥ�
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
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
        float ws = Input.GetAxis("Vertical");
        float ad = Input.GetAxis("Horizontal");
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
        //�����b������v��
        cameraTransform.Rotate(-mouseY * mouseSpeed, 0f, 0f);
    }

    private void JumpDetect()
    {
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 0.9f);
    }

    /// <summary>
    /// ���ʰ���
    /// </summary>
    private void InteractDetect()
    {
        aimSomething = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out aimedThing, 2f, interactableMask);
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
    #endregion
}
