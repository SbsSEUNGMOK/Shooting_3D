using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;          // ĳ���� ��Ʈ�ѷ�.
    public float moveSpeed;                         // �̵� �ӵ�.
    public float jumpHeight;                        // ������ ����
                                                    // .
    [Range(1f, 4f)]
    public float gravityScale;                      // �߷� ����.

    [Header("Ground")]
    public Transform groundChecker;                 // ������ üũ�ϴ� ��ġ
    public float groundRadius;                      // ��ŭ ũ��
    public LayerMask groundMask;                    // � ���� ������?

    [Header("Animator")]
    public Animator anim;                           // �ִϸ�����.


    float gravity = -9.81f;                         // �߷°�.
    Vector3 velocity;                               // �ӵ�.

    bool isGrounded;                                // ���鿡 ���ִ����� ���� ����.
    bool isWalk;                                    // �Ȱ� �ִ� ���ΰ�?
    bool isRun;                                     // �ٰ� �ִ� ���ΰ�?

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundRadius, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        bool isAccel = Input.GetKey(KeyCode.LeftShift);

        float x = Input.GetAxisRaw("Horizontal");  // ��,�� ����Ű�� �Է� �޾ƿ´�. (��:-1, X:0, ��:1)
        float z = Input.GetAxisRaw("Vertical");    // ��,�� ����Ű�� �Է� �޾ƿ´�. (��:-1, X:0, ��:1)
        float accel = isAccel ? 2.0f : 1.0f;
        Vector3 direction = (transform.right * x) + (transform.forward * z);    // Ű �Է� > ����

        isWalk = (direction != Vector3.zero) && !isAccel;                       // �ȴ� ���ΰ�?
        isRun =  (direction != Vector3.zero) && isAccel;                        // �ٴ� ���ΰ�?

        anim.SetBool("isWalk", isWalk);                                         // �ȴ� ���� ���� �Ķ���ͷ� ����.
        anim.SetBool("isRun", isRun);                                           // �ٴ� ���� ���� �Ķ���ͷ� ����.

        controller.Move(direction * moveSpeed * accel * Time.deltaTime);        // ���� ������

        // ����.
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityScale);
        }

        velocity.y += gravity * gravityScale * Time.deltaTime;                  // �߷� ���ӵ� �����ش�.
        controller.Move(velocity * Time.deltaTime);                             // �Ʒ� �������� �̵�.
    }

    private void OnDrawGizmosSelected()
    {
        if (groundChecker != null)
        {
            Gizmos.color = Color.green;     // ������ �׸��ǵ� ������ �ʷ��̴�.
            Gizmos.DrawWireSphere(groundChecker.position, groundRadius);
        }
    }
}