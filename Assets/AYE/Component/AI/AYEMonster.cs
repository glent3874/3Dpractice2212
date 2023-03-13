using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;
/// <summary>
/// <br>Distance 計算單位離自己的距離，取最短。</br>
/// <br>Find 是否有看見任何對向。</br>
/// <br>CanSee 偵測是否可以直視。</br>
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
/// <summary>適用於3D人形角色的AI底層，配合物理、RootMotion，基於AYEStatusBehaviour。</summary>
public class AYEMonster<StatusEnum> : AYEStatusBehaviour<StatusEnum> where StatusEnum : Enum
{
    public NavMeshAgent nav = null;
    public Animator anim = null;
    private void Reset()
    {
        GetAllComponent();
    }
    protected override void Start()
    {
        base.Start();
        GetAllComponent();
        nav.updatePosition = false;
        nav.updateRotation = false;
        nav.updateUpAxis = false;
    }
    void GetAllComponent()
    {
        if (nav == null)
            nav = this.gameObject.GetComponent<NavMeshAgent>();
        if (nav == null)
            nav = this.gameObject.AddComponent<NavMeshAgent>();
        if (anim == null)
            anim = this.gameObject.GetComponent<Animator>();
        if (anim == null)
            anim = this.gameObject.AddComponent<Animator>();
    }
    float faceForceTime = 0f;
    protected override void Update()
    {
        base.Update();
        nav.nextPosition = this.transform.position;
        // 在看某個東西時 如果角度太大就會轉身
        if (isFace)
        {
            // 失去物件就取消看東西
            if (tempLookTarget == null)
            {
                CancelFace();
                return;
            }
            Vector3 t = tempLookTarget.position + tempOffset;
            t.y = this.transform.position.y;
            if (Vector3.Angle(this.transform.forward, t - this.transform.position) > 80f)
                faceForceTime = 0.5f;
            if (faceForceTime > 0f)
            {
                Quaternion q = Quaternion.LookRotation(t - transform.position, Vector3.up);
                // 輔助視線速度只有3分之1視線速度。
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, Time.deltaTime * (tempSpeed * 0.33f));
                faceForceTime -= Time.deltaTime;
            }
        }
    }
    /// <summary>計算單位離自己的距離，取最短。</summary>
    public float Distance(params Vector3[] poss)
    {
        float d = 9999f;
        float n = 0f;
        for (int i = 0; i < poss.Length; i++)
        {
            n = Vector3.Distance(this.transform.position, poss[i]);
            if (n < d)
                d = n;
        }
        return d;
    }
    float lastFindTime = 0f;
    /// <summary>
    /// 判定是否能看見該對象
    /// </summary>
    /// <param name="cd">在Update時可以帶入cd節省性能</param>
    /// <param name="m">視線距離</param>
    /// <param name="angle">角度</param>
    /// <param name="eye">自身眼睛</param>
    /// <param name="player">輸出看到的對象(近到遠)</param>
    /// <param name="blockMask">會阻擋視線的圖層</param>
    /// <param name="canLookMask">目標要看的圖層</param>
    /// <returns>是否有看見任何對向</returns>
    public List<Collider> FindCanSeeLayer(float cd, float m, float angle, Transform eye, LayerMask blockMask, LayerMask canLookMask)
    {
        List<Collider> answer = new List<Collider>();
        if (cd > 0f && Time.time < lastFindTime + cd)
            return answer;
        lastFindTime = Time.time;
        // 圓形判定
        Collider[] overlapSphere = Physics.OverlapSphere(eye.position, m, canLookMask);
        // 檢定是否在扇形內
        for (int i = 0; i < overlapSphere.Length; i++)
        {
            float n = Vector3.Angle(eye.forward, overlapSphere[i].transform.position - eye.position);
            if (n < angle * 0.5f)
            {
                // 可以直視不被阻擋
                if (CanSee(eye.position, overlapSphere[i].transform.position, blockMask))
                {
                    answer.Add(overlapSphere[i]);
                }
            }
        }
        return answer;
    }
    float lastFindTagTime = 0f;
    /// <summary>回傳附近有相關標記的東西</summary>
    public Transform FindTag(float cd, Transform eye, string tagName, float m = 5f, float angle = 360f)
    {
        if (Time.time < lastFindTagTime + cd)
            return null;
        lastFindTagTime = Time.time;
        // 如果周邊有有趣的東西就盯著看
        Collider[] stuff = Physics.OverlapSphere(eye.position, m);
        for (int i = 0; i < stuff.Length; i++)
        {
            if (Vector3.Angle(eye.forward, stuff[i].transform.position - eye.transform.position) < angle * 0.5f)
            {
                if (stuff[i].tag == tagName)
                {
                    return stuff[i].transform;
                }
            }
        }
        return null;
    }
    float lastFindCanSeeTagTime = 0f;
    /// <summary>回傳附近有相關標記並且可以直視不受阻礙的東西</summary>
    public Transform FindCanSeeTag(float cd, Transform eye, string tagName, LayerMask blockMask, float m = 5f, float angle = 360f)
    {
        if (Time.time < lastFindCanSeeTagTime + cd)
            return null;
        lastFindCanSeeTagTime = Time.time;
        // 如果周邊有有趣的東西就盯著看
        Collider[] stuff = Physics.OverlapSphere(eye.position, m);
        for (int i = 0; i < stuff.Length; i++)
        {
            if (Vector3.Angle(eye.forward, stuff[i].transform.position - eye.transform.position) < angle * 0.5f)
            {
                if (stuff[i].tag == tagName)
                {
                    if (CanSee(eye.position, stuff[i].transform.position, blockMask))
                        return stuff[i].transform;
                }
            }
        }
        return null;
    }
    /// <summary>偵測是否可以直視</summary>
    public bool CanSee(Vector3 a, Vector3 b, LayerMask blockMask)
    {
        Vector3 direction = b - a;
        float maxDistance = Vector3.Distance(a, b);
        // 不被阻擋表示可以直視
        return !Physics.Raycast(a, direction, direction.magnitude, blockMask);
    }
    /// <summary>是否足夠近</summary>
    public bool Close(Vector3 pos, bool ignoreY, float range = 0.3f)
    {
        if (ignoreY)
            pos.y = this.transform.position.y;
        return Vector3.Distance(pos, this.transform.position) < range;
    }
    Vector3[] eight = new Vector3[]
    {
    new Vector3(0, 0, 1),
    new Vector3(0, 0, -1),
    new Vector3(-1, 0, 0),
    new Vector3(1, 0, 0),
    new Vector3(-0.75f, 0, 0.75f),
    new Vector3(0.75f, 0, 0.75f),
    new Vector3(-0.75f, 0, -0.75f),
    new Vector3(0.75f, 0, -0.75f),
    };
    float lastWayChange = 0f;
    Vector3 lastWay = Vector3.zero;
    /// <summary>設定移動的方向，直接用ws、ad值反饋給動畫系統，請務必將角色的應用設定八方。</summary>
    public void Way(Vector3 target, float rotateSpeed = 5f, float animMixSpeed = 5f)
    {
        // 導航
        nav.SetDestination(target);
        target = nav.steeringTarget;
        // 高度一致
        target.y = this.transform.position.y;
        // 如果沒有面向需求就讓自己逐漸配合轉向
        if (isFace == false)
        {
            Quaternion q = Quaternion.LookRotation(target - transform.position, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, Time.deltaTime * rotateSpeed);
            SetWSADAnim(Vector2.up);
        }
        else
        {
            // 計算出相對位置
            Vector3 ab = target - this.transform.position;
            // 將世界方向轉為本地方向
            Vector3 localab = this.transform.InverseTransformDirection(ab);
            // 限定長度成為向量
            localab = Vector3.ClampMagnitude(localab * 999f, 1f);
            float d = 999f;
            int win = 0;
            for (int i = 0; i < eight.Length; i++)
            {
                float n = Vector3.Distance(eight[i], localab);
                if (n < d)
                {
                    d = n;
                    win = i;
                }
            }
            localab = eight[win];
            if (lastWay != localab && Time.time > lastWayChange + 0.5f)
            {
                lastWay = localab;
                lastWayChange = Time.time;
            }
            // 讀取動畫原本的數值
            Vector2 wsad = new Vector2(anim.GetFloat("ad"), anim.GetFloat("ws"));
            // 漸變
            wsad = Vector2.Lerp(wsad, new Vector2(lastWay.x, lastWay.z), Time.deltaTime * animMixSpeed);
            // 動畫輸出
            SetWSADAnim(wsad);
        }
    }
    Vector2 lastWSAD = Vector2.zero;
    void SetWSADAnim(Vector2 wsad)
    {
        if (lastWSAD != wsad)
        {
            lastWSAD = wsad;
            anim.SetFloat("ad", wsad.x);
            anim.SetFloat("ws", wsad.y);
        }
    }
    Transform tempLookTarget = null;
    Vector3 tempOffset = Vector3.zero;
    float tempSpeed = 0f;
    /// <summary>取消看東西時頭轉回來的速度</summary>
    public float stopFaceSpeed = 2f;
    bool isFace = false;
    /// <summary>面向某個東西</summary>
    public void Face(Transform target, float speed = 3f)
    {
        Face(target, Vector3.zero, speed);
    }
    /// <summary>面向某個東西</summary>
    public void Face(Transform target, Vector3 offset, float speed = 3f)
    {
        tempSpeed = speed;
        tempLookTarget = target;
        this.tempOffset = offset;
        isFace = true;
    }
    /// <summary>取消面向某個東西</summary>
    public void CancelFace()
    {
        isFace = false;
    }
    /// <summary>視線權重</summary>
    float lookAtWeight = 0f;
    /// <summary>看的目標</summary>
    Vector3 lookPoint = Vector3.zero;
    private void OnAnimatorIK(int layerIndex)
    {
        if (isFace)
            lookAtWeight = Mathf.Lerp(lookAtWeight, 1f, Time.deltaTime * tempSpeed);
        else
            lookAtWeight = Mathf.Lerp(lookAtWeight, 0f, Time.deltaTime * stopFaceSpeed);
        anim.SetLookAtWeight(lookAtWeight);
        if (tempLookTarget != null)
            anim.SetLookAtPosition(tempLookTarget.position + tempOffset);
    }
}

