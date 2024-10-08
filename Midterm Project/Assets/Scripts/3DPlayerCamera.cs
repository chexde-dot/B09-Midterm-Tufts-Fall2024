using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    public Transform playerObj;
    public float speed = 0.01f;

    [SerializeField]
    public Vector3 offsetCam;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            playerObj = GameObject.FindWithTag("Player").transform;
        }
    }

    private void LateUpdate()
    {
        MoveAndRotateCamera();
    }

    public void MoveAndRotateCamera()
    {
        Vector3 offsetCameraSide = new Vector3(offsetCam.x + 1, offsetCam.y, offsetCam.z);
        //if (playerObj == null) { Debug.LogWarning("Missing playerObj ref !", this); return; }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = playerObj.TransformPoint(offsetCameraSide);
        }
        else { transform.position = playerObj.position + offsetCameraSide; }

        // compute rotation
        if (lookAt) { transform.LookAt(playerObj); }
        //else {transform.rotation = playerObj.rotation;}
        else
        {
            Transform fromRot = gameObject.transform;
            Transform toRot = playerObj;
            transform.rotation = Quaternion.Lerp(fromRot.rotation, toRot.rotation, Time.time * speed);
        }
    }
}