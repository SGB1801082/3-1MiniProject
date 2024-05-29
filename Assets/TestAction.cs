using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;

    public float speed;
    private float h, v;
    private string lastInputDirection = "None";

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        UpdateDirection();
        Move();
        PlayAnimation();
    }

    private void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    /*private void UpdateDirection()
    {
        if (h != 0)
            lastInputDirection = "Horizontal";
        else if (v != 0)
            lastInputDirection = "Vertical";
    }*/
    private void UpdateDirection()
    {
        // 수평 입력이 있는 경우
        if (h != 0)
        {
            lastInputDirection = "Horizontal";
        }
        // 수직 입력이 있는 경우
        else if (v != 0)
        {
            lastInputDirection = "Vertical";
        }
        // 둘 다 없는 경우
        else
        {
            lastInputDirection = "None";
        }
    }


    /*private void Move()
    {
        Vector2 moveVec = Vector2.zero;

        // 플레이어가 동시에 수평 및 수직 입력을 주었을 때, 이동 방향을 결정합니다.
        moveVec.x = h; // 수평 입력을 x 방향으로 적용합니다.
        moveVec.y = v; // 수직 입력을 y 방향으로 적용합니다.

        rigid.velocity = moveVec.normalized * speed; // 정규화된 이동 벡터를 사용합니다.

        // 입력이 없을 때 마지막 입력 방향을 초기화합니다.
        if (h == 0 && v == 0)
            lastInputDirection = "None";
    }*/

    private void Move()
    {
        Vector2 moveVec = Vector2.zero;

        // 플레이어가 수평 및 수직 입력을 모두 주었을 때, 이동 방향을 결정합니다.
        if (lastInputDirection == "Horizontal")
        {
            moveVec.x = h;
            moveVec.y = v; // 수평 입력이 주어진 상황에서 수직 입력을 고려합니다.
        }
        else if (lastInputDirection == "Vertical")
        {
            moveVec.x = h; // 수직 입력이 주어진 상황에서 수평 입력을 고려합니다.
            moveVec.y = v;
        }

        rigid.velocity = moveVec * speed;

        // 입력이 없을 때 마지막 입력 방향을 초기화합니다.
        if (h == 0 && v == 0)
            lastInputDirection = "None";
    }


    private void PlayAnimation()
    {
        if (animator.GetInteger("hAxisRaw") != (int)h)
        {
            animator.SetInteger("hAxisRaw", (int)h);
            animator.SetBool("isChange", true);
        }
        else if (animator.GetInteger("vAxisRaw") != (int)v)
        {
            animator.SetInteger("vAxisRaw", (int)v);
            animator.SetBool("isChange", true);
        }
        else
        {
            animator.SetBool("isChange", false);
        }
    }
}
