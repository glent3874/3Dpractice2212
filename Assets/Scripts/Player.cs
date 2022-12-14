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
    [SerializeField] Transform 水平旋轉軸 = null;

    float ws = 0f;
    float ad = 0f;
    float speed = 0f;
    float mouseX = 0f;
    float mouseY = 0f;
    float mouseYTotal = 0f;
    RaycastHit aimedThing;
    bool aimSomething;
    //視線看到的東西
    RaycastHit thingInSight;
    bool sight;
    //從攝影機到玩家的距離
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

    #region 事件
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
        ws = Mathf.Lerp(ws, Input.GetAxisRaw("Vertical"), Time.deltaTime * 10f);
        ad = Mathf.Lerp(ad, Input.GetAxisRaw("Horizontal"), Time.deltaTime * 10f);
        Vector3 move = new Vector3(ad * moveSpeed, rb.velocity.y, ws * moveSpeed);
        move.x = ad * Mathf.Lerp(moveSpeed, runSpeed, speed);
        move.y = rb.velocity.y;
        move.z = ws * Mathf.Lerp(moveSpeed, runSpeed, speed);

        //跑步
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, 1f, Time.deltaTime * 10f);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 10f);
        }

        //從世界座標轉換成水平旋轉軸的座標
        Vector3 轉換座標 = 水平旋轉軸.transform.TransformVector(move);
        rb.velocity = 轉換座標;

        //跳躍
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = Vector3.up * jumpPower;
        }

        //視角
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseYTotal += mouseY * -1f;
        mouseYTotal = Mathf.Clamp(mouseYTotal, -80f, 80f);
        Quaternion rotateY = Quaternion.Euler(mouseYTotal, 0f, 0f);
        //水平軸旋轉水平旋轉軸
        水平旋轉軸.transform.Rotate(0f, mouseX, 0f);
        //垂直軸旋轉垂直旋轉軸
        cameraFollow.localRotation = rotateY;
    }

    /// <summary>
    /// 跳躍偵測
    /// </summary>
    private void JumpDetect()
    {
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 0.9f);
    }

    /// <summary>
    /// 互動偵測
    /// </summary>
    private void InteractDetect()
    {
        //攝影機到玩家的距離
        cameraToPlayerDistance = Vector3.Distance(cameraTransform.position, cameraFollow.position);
        //從攝影機射出偵測到物件
        aimSomething = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out aimedThing, cameraToPlayerDistance + 2f, interactableMask);

        //IK控制:角色看向準心瞄準處
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

    private void KyleAnimate()
    {
        kyleAnimator.SetFloat("WS", ws);
        kyleAnimator.SetFloat("AD", ad);
        kyleAnimator.SetFloat("SPEED", speed);
    }
    #endregion
}
