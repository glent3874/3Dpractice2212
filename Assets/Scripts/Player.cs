using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [SerializeField] Rigidbody rb = null;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float mouseSpeed = 5f;

    #endregion

    #region 事件
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region 方法
    private void Move()
    {
        //移動
        float ws = Input.GetAxis("Vertical");
        float ad = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ad * moveSpeed, rb.velocity.y, ws * moveSpeed);

        //從世界座標轉換成此物件的座標
        Vector3 轉換座標 = this.transform.TransformVector(move);
        rb.velocity = 轉換座標;

        //視角
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //水平軸旋轉此物件
        this.transform.Rotate(0f, mouseX * mouseSpeed, 0f);
        //垂直軸旋轉攝影機
        cameraTransform.Rotate(-mouseY * mouseSpeed, 0f, 0f);
    }
    #endregion
}
