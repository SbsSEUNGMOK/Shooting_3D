using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;          // 캐릭터 컨트롤러.
    public float moveSpeed;                         // 이동 속도.
    public float jumpHeight;                        // 점프의 높이
                                                    // .
    [Range(1f, 4f)]
    public float gravityScale;                      // 중력 비율.

    [Header("Ground")]
    public Transform groundChecker;                 // 땅인지 체크하는 위치
    public float groundRadius;                      // 얼만큼 크게
    public LayerMask groundMask;                    // 어떤 것을 땅으로?

    [Header("Animator")]
    public Animator anim;                           // 애니메이터.


    float gravity = -9.81f;                         // 중력값.
    Vector3 velocity;                               // 속도.

    bool isGrounded;                                // 지면에 서있는지에 대한 여부.
    bool isWalk;                                    // 걷고 있는 중인가?
    bool isRun;                                     // 뛰고 있는 중인가?

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

        float x = Input.GetAxisRaw("Horizontal");  // 좌,우 방향키를 입력 받아온다. (좌:-1, X:0, 우:1)
        float z = Input.GetAxisRaw("Vertical");    // 상,하 방향키를 입력 받아온다. (하:-1, X:0, 상:1)
        float accel = isAccel ? 2.0f : 1.0f;
        Vector3 direction = (transform.right * x) + (transform.forward * z);    // 키 입력 > 방향

        isWalk = (direction != Vector3.zero) && !isAccel;                       // 걷는 중인가?
        isRun =  (direction != Vector3.zero) && isAccel;                        // 뛰는 중인가?

        anim.SetBool("isWalk", isWalk);                                         // 걷는 상태 값을 파라미터로 전달.
        anim.SetBool("isRun", isRun);                                           // 뛰는 상태 값을 파라미터로 전달.

        controller.Move(direction * moveSpeed * accel * Time.deltaTime);        // 실제 움직임

        // 점프.
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityScale);
        }

        velocity.y += gravity * gravityScale * Time.deltaTime;                  // 중력 가속도 더해준다.
        controller.Move(velocity * Time.deltaTime);                             // 아래 방향으로 이동.
    }

    private void OnDrawGizmosSelected()
    {
        if (groundChecker != null)
        {
            Gizmos.color = Color.green;     // 도형을 그린건데 색상은 초록이다.
            Gizmos.DrawWireSphere(groundChecker.position, groundRadius);
        }
    }
}