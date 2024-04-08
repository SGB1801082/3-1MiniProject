using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    //1차
    [SerializeField] private float speed;
    private float h, v;
    Rigidbody2D rigid;

    //2차
    private bool isHorizontalMove;
    private Animator animator;
    private Vector3 dirVec;// Ray를 발사하고 충돌을 체크할때 사용할, 현재 바라보고 있는 방향 값을 가진 변수 선언 
    private GameObject scanRayObjcet;// Ray에 충돌한 오브젝트를 저장할 변수

    //3차
    public GameUiMgr gUiMgr;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {

        // Move Value //플레이어가 대화 상호작용 중일때에는 캐릭터가 움직이지 못 하도록 isActionTalk값이 없을때만 움직일 수 있도록 코드 수정
        h = gUiMgr.isActionTalk ? 0 : Input.GetAxisRaw("Horizontal");
        v = gUiMgr.isActionTalk ? 0 : Input.GetAxisRaw("Vertical");

        // Check Button Down & Up //플레이어가 대화 상호작용 중일때에는 캐릭터가 움직이지 못 하도록 isActionTalk값이 없을때만 움직일 수 있도록 코드 수정
        bool hDown = gUiMgr.isActionTalk ? false : Input.GetButtonDown("Horizontal");// 오른쪽
        bool vDown = gUiMgr.isActionTalk ? false : Input.GetButtonDown("Vertical");// 아래
        bool hUp = gUiMgr.isActionTalk ? false : Input.GetButtonDown("Horizontal");// 왼쪽
        bool vUp = gUiMgr.isActionTalk ? false : Input.GetButtonDown("Vertical");// 위

        // Check Horizontal Move
        if (hDown)
        {
            isHorizontalMove = true;
        }
        else if (vDown)
        {
            isHorizontalMove = false;
        }
        else if (hUp || vUp) // 양쪽(ex +h, -h) 버튼을 누른 상태로 한 쪽만 버튼 업(+h)이면 발생하는 문제를 해결하기위함
        {
            isHorizontalMove = (h != 0);// 현재 속도를 측정하여 체크함
        }

        //Animation
        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetInteger("hAxisRaw", (int)h);
            animator.SetBool("isChange", true);
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            animator.SetBool("isChange", false);
        }


        //Direction == 플레이어가 바라보는 방향
        // 특정 방향으로 이동중, 키를 떼지않고 다른방향으로 이동하다가 키를 떼고, 특정방향으로 계속 움직일 경우 레이가 잠시 이동했던 방향으로 일시적으로 고정되는 현상 발생 03-10
        if (vDown && v > 0) // 위를 보고있을때
        {
            dirVec = Vector3.up;// 레이저 위로 발사
        }
        else if (vDown && v < 0) // 아래를 보고있을때
        {
            dirVec = Vector3.down;
        }
        else if (hDown && h < 0) // 왼쪽를 보고있을때
        {
            dirVec = Vector3.left;
        }
        else if (hDown && h > 0) // 오른쪽를 보고있을때
        {
            dirVec = Vector3.right;
        }


        // Scan Ray Object
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && scanRayObjcet != null)
        {
            //Debug.Log("This is : "+ scanRayObjcet.name);// 레이에 맞고 스페이스바를 통해 상호작용하여 Scan Object에 저장된 물체가 있을 시 오브젝트 이름 출력 
            gUiMgr.TalkAction(scanRayObjcet);// Ray로 상호작용한 Object의 정보를 twonMgr로 넘겨서 그곳에 있는 대화창에 정보를 출력하게 함.
        }

    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizontalMove ? new Vector2(h, 0) : new Vector2(0, v);
        //rigid.velocity = new Vector2(h, v) * speed;
        rigid.velocity = moveVec * speed;

        // 다른 오브젝트와의 충돌을 체크하기위한 Ray
        Debug.DrawRay(rigid.position, dirVec * 0.8f, new Color(0, 1, 0));// DrawRay (Scene창에서만 보임)를 발사하는데, 현재 캐릭터의 위치값, 길이(float), 색갈(new Color)이 필요
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.8f, LayerMask.GetMask("Object"));// 마스크가 Object인 경우에만 충돌(이미 플레이어가 사용중인 콜라이더와의 충돌을 피하기 위함.)

        // 레이가 오브젝트와 충돌할 시 ScanObject에 저장 
        if (rayHit.collider != null)
        {
            scanRayObjcet = rayHit.collider.gameObject;// RayCast된 오브젝트를 변수로 저장하여 활용 할 수 있게함
        }
        else
        {
            scanRayObjcet = null;// 충돌이없다면 scanObj는 비워져야함.
        }

    }

}
