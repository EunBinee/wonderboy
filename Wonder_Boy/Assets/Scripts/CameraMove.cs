using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    public float offsetY = 1f;
    public float offsetZ = -10f;
    public float smooth = 5f;

    Vector3 target;

    void LateUpdate()
    {
        target = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.y + offsetZ);
        transform.position= Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
    }
}
