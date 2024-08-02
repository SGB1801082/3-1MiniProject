using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;

    float h, v;
    Vector2 moveDirection;

    private bool prevIsMovingLeft, prevIsMovingRight, prevIsMovingUp, prevIsMovingDown, prevIsIdle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 입력 값을 확인
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        // 이동 방향 설정
        h = (right ? 1 : 0) - (left ? 1 : 0);
        v = (up ? 1 : 0) - (down ? 1 : 0);

        // 애니메이션 상태 업데이트
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(h, v).normalized;
        if (!GameUiMgr.single.isActionTalk)
        {
            rigid.velocity = moveDirection * 4f;
        }
    }

    void UpdateAnimation()
    {
        bool isMovingLeft = h < 0;
        bool isMovingRight = h > 0;
        bool isMovingUp = v > 0;
        bool isMovingDown = v < 0;
        bool isIdle = !isMovingLeft && !isMovingRight && !isMovingUp && !isMovingDown;

        // 애니메이터의 파라미터 업데이트
        animator.SetBool("IsMovingLeft", isMovingLeft);
        animator.SetBool("IsMovingRight", isMovingRight);
        animator.SetBool("IsMovingUp", isMovingUp);
        animator.SetBool("IsMovingDown", isMovingDown);
        animator.SetBool("IsIdle", isIdle);

        // 애니메이션 상태에 따라 캐릭터의 이동을 조정
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Player_Left_Walk"))
        {
            // Left 애니메이션이 실행 중일 때
            h = -1; // 왼쪽 이동
            v = 0;  // 상하 이동 없음
        }
        else if (stateInfo.IsName("Player_Right_Walk"))
        {
            // Right 애니메이션이 실행 중일 때
            h = 1; // 오른쪽 이동
            v = 0;  // 상하 이동 없음
        }
        else if (stateInfo.IsName("Player_Up_Walk"))
        {
            // Up 애니메이션이 실행 중일 때
            h = 0; // 좌우 이동 없음
            v = 1; // 위쪽 이동
        }
        else if (stateInfo.IsName("Player_Down_Walk"))
        {
            // Down 애니메이션이 실행 중일 때
            h = 0; // 좌우 이동 없음
            v = -1; // 아래쪽 이동
        }
        else if (stateInfo.IsName("Player_Idle"))
        {
            // Idle 애니메이션이 실행 중일 때
            h = 0;
            v = 0;
        }

        // 디버그 로그 출력
        if (isMovingLeft != prevIsMovingLeft || isMovingRight != prevIsMovingRight ||
            isMovingUp != prevIsMovingUp || isMovingDown != prevIsMovingDown || isIdle != prevIsIdle)
        {
            Debug.Log($"Left: {isMovingLeft}, Right: {isMovingRight}, Up: {isMovingUp}, Down: {isMovingDown}, Idle: {isIdle}");
        }

        // 이전 상태 업데이트
        prevIsMovingLeft = isMovingLeft;
        prevIsMovingRight = isMovingRight;
        prevIsMovingUp = isMovingUp;
        prevIsMovingDown = isMovingDown;
        prevIsIdle = isIdle;
    }
}
