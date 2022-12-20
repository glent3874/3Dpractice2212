using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject cinamachine = null;

    public void StartAnimation()
    {
        cinamachine.SetActive(true); 
    }

    public void EndAnimation()
    {
        Destroy(cinamachine);
        Destroy(this.gameObject);
    }
}
