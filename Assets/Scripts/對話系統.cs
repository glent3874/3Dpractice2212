using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 對話系統 : MonoBehaviour
{
    [SerializeField] Animator 動畫 = null;
    [SerializeField] NpcData 測試用文本 = null;
    [SerializeField] Text 內文 = null;

    private void Update()
    {
        //如果在Unity編輯器中執行
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            開始對話(測試用文本);
        }
#endif

        if (Input.GetKeyDown(KeyCode.E) && stop) 
        {
            stop = false;
        }
    }

    public void 開始對話(NpcData 文本)
    {
        StartCoroutine(對話中(文本));
    }

    IEnumerator 對話中(NpcData 文本)
    {
        內文.text = "";
        動畫.SetBool("啟動", true);
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 文本.對話列表.Count; i++)
        {
            string 要講的話 = "";
            for (int j = 0; j < 文本.對話列表[i].內容.Length; j++)
            {
                char 新的字 = 文本.對話列表[i].內容[j];
                要講的話 = 要講的話 + 新的字;

                內文.text = 要講的話;
                yield return new WaitForSeconds(0.1f);
            }
            //對話跑完停下來
            stop = true;

            while (stop) 
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        動畫.SetBool("啟動", false);
    }

    bool stop
    {
        get { return _stop; }
        set 
        { 
            _stop = value;
            if(_stop)
            {
                繼續提示.localScale = Vector3.one;
            }
        }
    }
    bool _stop = false;

    [SerializeField] Transform 繼續提示 = null;
}