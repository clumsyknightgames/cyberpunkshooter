using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;

    public const float offsetY = 16;
    public const float offsetZ = -16;

    private Vector3 camPos;
	void Update ()
    {
        camPos.x = followTarget.transform.position.x;
        camPos.y = followTarget.transform.position.y + offsetY;
        camPos.z = followTarget.transform.position.z + offsetZ;

        transform.position = camPos;
    }
}
