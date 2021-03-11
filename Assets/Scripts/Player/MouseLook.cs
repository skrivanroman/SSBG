using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=_QajrabyTJc
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private float cameraRotationX = 0f;
    private Transform player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
