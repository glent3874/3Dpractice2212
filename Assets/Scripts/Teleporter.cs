using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform teleportTarget;
    [SerializeField] GameObject player;
    [SerializeField] GameObject 水平旋轉軸;
    [SerializeField] Transform lookAt;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.transform.position = teleportTarget.position;
            lookAt.position = lookAt.position + new Vector3(0f, player.transform.position.y - lookAt.position.y, 0f);
            水平旋轉軸.transform.LookAt(lookAt);
        }
    }
}
