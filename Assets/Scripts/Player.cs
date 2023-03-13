using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家
/// </summary>
public class Player : MonoBehaviour
{
    #region 單例模式
    public static Player instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region 欄位
    [SerializeField] Rigidbody rb = null;                       //物理系統
    [SerializeField] public Transform cameraFollow = null;             //攝影機跟隨的目標(垂直旋轉軸)
    [SerializeField] Transform cameraTransform = null;          //攝影機的座標
    [SerializeField] float moveSpeed = 1.5f;                    //移動速度
    [SerializeField] float runSpeed = 5f;                       //跑步速度
    [SerializeField] float mouseSpeed = 5f;                     //鏡頭靈敏度
    [SerializeField] float jumpPower = 8f;                      //跳躍力
    [SerializeField] LayerMask interactableMask;                //可互動的圖層
    [SerializeField] Transform interactUI;                      //互動UI
    [SerializeField] LayerMask seenableMask;                    //視線可跟隨的圖層(雷射來自攝影機)
    [SerializeField] IKControll ikControl = null;               //IK控制
    [SerializeField] Animator kyleAnimator = null;              //角色動畫控制器
    [SerializeField] public Transform 水平旋轉軸 = null;         //角色水平旋轉的軸心
    [SerializeField] public Transform eyes = null;

    float ws = 0f;
    float ad = 0f;
    float speed = 0f;
    float mouseX = 0f;
    float mouseY = 0f;
    public float mouseYTotal = 0f;
    RaycastHit aimedThing;                                      //視線瞄準的可互動物件
    bool aimSomething;                                          //是否有瞄準到可互動物件
    RaycastHit thingInSight;                                    //視線瞄準的物件
    bool sight;                                                 //是否有看到物件
    float cameraToPlayerDistance;                               //從攝影機到玩家的距離

    /// <summary>
    /// 同時設定角色跳躍動畫
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

    #region 事件
    private void Start()
    {
        //有無遊戲資料的判定
        if (SaveManager.instance.continueGame == true)
        {
            this.transform.position = SaveManager.instance.saveData.playerPos;                      //玩家的位置

            水平旋轉軸.localRotation = Quaternion.Euler(0f, SaveManager.instance.saveData.playerRotate.y, 0f);
            mouseYTotal = SaveManager.instance.saveData.playerRotate.x;

            SaveManager.instance.continueGame = false;                                              //已載入遊戲就取消
        }
        Cursor.lockState = CursorLockMode.Locked;       //鎖定滑鼠
    }

    private void Update()
    {
        KyleAnimate();                      //角色動畫系統
        Move();                             //移動
        Interact();                         //互動
    }

    /// <summary>
    /// 不能lag的物理系統
    /// </summary>
    private void FixedUpdate()
    { 
        InteractDetect();               //互動偵測
        JumpDetect();                   //跳躍偵測
    }

    /// <summary>
    /// 最後運行的更新
    /// </summary>
    private void LateUpdate()
    {
        InteractUI();                   //互動UI
    }
    #endregion

    #region 方法
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //移動
        ws = Mathf.Lerp(ws, Input.GetAxisRaw("Vertical"), Time.deltaTime * 10f);        //取得垂直(WS)輸入值
        ad = Mathf.Lerp(ad, Input.GetAxisRaw("Horizontal"), Time.deltaTime * 10f);      //取得水平(AD)輸入值
        Vector3 move;
        move.x = ad * Mathf.Lerp(moveSpeed, runSpeed, speed);                           //在走路與跑步速度間使用漸進值
        move.y = rb.velocity.y;
        move.z = ws * Mathf.Lerp(moveSpeed, runSpeed, speed);                           //在走路與跑步速度間使用漸進值

        //按住shift跑步
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
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.timeScale;                //使頭部能受到遊戲速度控制
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.timeScale;
        mouseYTotal += mouseY * -1f;                                                    //滑鼠垂直移動量疊加
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
        onGround = Physics.Raycast(this.transform.position, Vector3.down, 1.2f);        //從玩家中心向下發射雷射偵測地板
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
            aimedThing.collider.transform.root.GetComponent<Interactable>().Interact();
        }
    }

    /// <summary>
    /// 移動動畫
    /// </summary>
    private void KyleAnimate()
    {
        kyleAnimator.SetFloat("WS", ws);
        kyleAnimator.SetFloat("AD", ad);
        kyleAnimator.SetFloat("SPEED", speed);
    }
    #endregion
}
