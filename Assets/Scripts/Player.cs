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

    #region 欄位
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

    #region 事件
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
    /// 不能lag的物理系統
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

    #region 方法
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //移動
        float ws = Input.GetAxis("Vertical");
        float ad = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ad * moveSpeed, rb.velocity.y, ws * moveSpeed);

        //從世界座標轉換成此物件的座標
        Vector3 轉換座標 = this.transform.TransformVector(move);
        rb.velocity = 轉換座標;

        //跳躍
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        //視角
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //水平軸旋轉此物件
        this.transform.Rotate(0f, mouseX * mouseSpeed, 0f);
        //垂直軸旋轉攝影機
        cameraTransform.Rotate(-mouseY * mouseSpeed, 0f, 0f);
    }

    private void JumpDetect()
    {
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 0.9f);
    }

    /// <summary>
    /// 互動偵測
    /// </summary>
    private void InteractDetect()
    {
        aimSomething = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out aimedThing, 2f, interactableMask);
    }

    /// <summary>
    /// 互動UI的開關
    /// </summary>
    private void InteractUI()
    {
        if (aimSomething) interactUI.localScale = Vector3.one;
        else interactUI.localScale = Vector3.zero;
    }

    /// <summary>
    /// 互動
    /// </summary>
    private void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && aimSomething && !對話系統.instance.isPlay)
        {
            aimedThing.collider.transform.root.GetComponent<Item>().interact();
        }
    }
    #endregion
}
