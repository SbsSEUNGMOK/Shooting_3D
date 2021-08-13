using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;            // 플레이어의 몸
    public float mouseSensitivity = 0f;
    float xRorate = 0f;                     // 위, 아래로 보는 각도.

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 커서 잠금.

        // Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제.
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRorate -= mouseY;
        xRorate = Mathf.Clamp(xRorate, -60.0f, 60.0f);

        transform.localRotation = Quaternion.Euler(xRorate, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
