using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }

}
