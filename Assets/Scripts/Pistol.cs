using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Controller
{
    [SerializeField] public GameObject mainCam;

    public override void Interact()
    {
        base.Interact();

        if (¤¬°Ê¦¸¼Æ > 1)
        {
            mainCam.GetComponent<Shoot>().havePistol = true;
        }
    }
}
