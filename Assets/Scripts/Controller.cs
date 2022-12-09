using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, Item
{
    public void interact()
    {
        Destroy(this.gameObject);
    }
}
