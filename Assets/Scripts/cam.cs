using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3; // чувствительность мышки
    public float distanse = 10;
    private float X, Y;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        offset = new Vector3(offset.x, offset.y, -distanse);
    }

    void Update()
    {
        X +=  Input.GetAxis("Mouse X") * sensitivity;
        Y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
        target.transform.localEulerAngles = new Vector3(-Y, X, 0);
    }
}