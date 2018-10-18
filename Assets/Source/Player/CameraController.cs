////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : CameraController.cs
//   Description :
//      Class used to make the camera look at the player and any other
//      Camera logic used for the game
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;

    public const float offsetY = 16;
    public const float offsetZ = -16;

    private float zoom = 512;
    private const float zoomStep = 128;

    private Vector3 camPos;
	void Update ()
    {
        float deltaZoom = Input.GetAxis("Zoom") * zoomStep * Time.deltaTime;
        zoom += deltaZoom;

        // Debug.Log("Camera zoom: " + zoom.ToString());

        camPos.x = followTarget.transform.position.x;
        camPos.y = followTarget.transform.position.y + Mathf.Sqrt(zoom / 2);
        camPos.z = followTarget.transform.position.z - Mathf.Sqrt(zoom / 2);

        transform.position = camPos;
    }
}
