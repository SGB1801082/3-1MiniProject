using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BaseEntity : MonoBehaviour
{
    protected float max_Hp;
    protected float cur_Hp;
    protected float max_Mp;
    protected float cur_Mp;
    protected float atkDmg;
    protected float atkSpd;
    protected float atkRange;
    protected bool isAttack = false;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    // 가까이에 있는 적을 타겟하는 메소드

    public GameObject FindTarget()
    {
        string targetTag = (tag == "Player") ? "Enemy" : "Player";
        // 타겟 오브젝트 배열 찾기
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        // 현재 위치
        Vector3 currentPosition = transform.position;
        // 가장 가까운 대상 찾기
        GameObject nearestTarget = targets.OrderBy(target => Vector3.Distance(currentPosition, target.transform.position)).FirstOrDefault();
        // 찾은 대상 반환
        return nearestTarget;
    }


    // 1초마다 타겟을 업데이트 하는 메소드
    public IEnumerator UpdateTarget()
    {
        if (FindTarget() != null) 
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f); // 1초 대기
                FindTarget();
            }
        }
        
    }

    // Idle 상태이거나 Attack 상태일때 최대한 피할수 있게 우선순위 높히는 메서드 ( NavMeshPlus 에셋 관련 )
    public void SetMovementPriority(bool isMoving)
    {
        int priority = isMoving ? 50 : 30; // 이동 중이면 우선순위를 50으로, 아니면 30로 설정
        agent.avoidancePriority = priority;
    }

    
    // 타겟으로 향해 이동하는 메서드 ( NavMeshPlus 이용 )
    public void MoveToTarget()
    {
        Transform target = FindTarget().transform;
        if(target != null) 
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            SetMovementPriority(true);
        }
        else
        {
            return;
        }
        
    }

    // 공격 사거리에 들어오면 이동 멈추고 공격 준비
    public void StopMove()
    {
        if (isAttack)
        {
            agent.isStopped = true;
            SetMovementPriority(false);
        }
    }


    // 공격 사거리에 오면 논리형으로 True or False 반환하는 메서드
    public bool IsAttack(float range)
    {
       
        Transform target = FindTarget().transform;

        Vector2 tVec = (Vector2)(target.localPosition - transform.position);
        float tDis = tVec.sqrMagnitude;

        if (tDis <= atkRange * atkRange)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }

        return isAttack;
    }

    public IEnumerator Attack()
    {
        BaseEntity target = FindTarget().GetComponent<BaseEntity>();

        if (target != null)
        {
            while (true)
            {
                yield return new WaitForSeconds(atkSpd);
                Debug.Log("공격함");
                Debug.Log("대상 체력이 줄어듦" + target.gameObject + " " + (target.cur_Hp -= atkDmg));

                if (target.cur_Hp <= 0)
                {
                    yield break;
                }
            }      
        }
     
    }
}
