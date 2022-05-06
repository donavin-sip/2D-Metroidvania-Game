using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    private float distance = 40.0f;     // distance from player

    // Update is called once per frame
    void Update()
    {
        // camera follows the player, avoids rotation of the camera from the player
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
    }
}
