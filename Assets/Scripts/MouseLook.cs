using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;            // �÷��̾��� ��
    public float mouseSensitivity = 0f;
    float xRorate = 0f;                     // ��, �Ʒ��� ���� ����.

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Ŀ�� ���.

        // Cursor.lockState = CursorLockMode.None; // Ŀ�� ��� ����.
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
