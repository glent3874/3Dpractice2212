using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [SerializeField] Rigidbody rb = null;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float mouseSpeed = 5f;

    #endregion

    #region �ƥ�
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region ��k
    private void Move()
    {
        //����
        float ws = Input.GetAxis("Vertical");
        float ad = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ad * moveSpeed, rb.velocity.y, ws * moveSpeed);

        //�q�@�ɮy���ഫ�������󪺮y��
        Vector3 �ഫ�y�� = this.transform.TransformVector(move);
        rb.velocity = �ഫ�y��;

        //����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //�����b���হ����
        this.transform.Rotate(0f, mouseX * mouseSpeed, 0f);
        //�����b������v��
        cameraTransform.Rotate(-mouseY * mouseSpeed, 0f, 0f);
    }
    #endregion
}
