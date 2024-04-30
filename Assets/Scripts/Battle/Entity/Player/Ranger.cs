using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UI;

public class Ranger : BaseEntity
{
    private EntityStat stat;
    Transform cur_target;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Ranger 생성");

        // 고유 id, 최대 HP, 최대 MP, 공격력, 공격속도, 공격사거리 순으로 초기화
        stat = new EntityStat
            (1, 15, 5, 2, 1, 8, false);

        entity_id = stat.id;
        max_Hp = stat.max_Hp;
        cur_Hp = max_Hp;
        max_Mp = stat.max_Mp;
        cur_Mp = 0;
        atkDmg = stat.atkDmg;
        SetAttackSpeed(stat.atkSpd);
        atkRange = stat.atkRange;
        able_Skill = stat.isSkill;
        isMelee = true; // 임시로 근접 유닛과 똑같은 방식으로 공격 추후에 투사체를 발사하는 방식으로 바꿀 예정
    }

    protected override void Update()
    {
        base.Update();
        if (_curstate == State.Skill)
        {
            Skill();
        }
        cur_target = target;
    }


    public void Skill()
    {
        if (_curstate == State.Skill)
        {
            
            StopAllCoroutines();
            if (isAttack)
            {
                
                BaseEntity target = FindTarget().GetComponent<BaseEntity>();
                Debug.Log("타겟의 적에게 2배의 데미지로 한번 공격" + " " + (atkDmg * 2) + "데미지");
                target.cur_Hp -= atkDmg * 2;
                Debug.Log(target.cur_Hp + " " + target.name);
            }
            else
            {
                return;
            }
            cur_Mp = 0;
            ChangeState(State.Idle);
        }
    }

    public void Arrow()
    {
        if (_curstate == State.Attack) 
        {
            StartCoroutine(ArrowCoroutine(cur_target));
        }
    }

    private IEnumerator ArrowCoroutine(Transform target)
    {
        GameObject arrow = BattleManager.Instance.pool.GetObject(0);
        arrow.transform.position = transform.position;

        


        if (target != null)
        {
            Vector3 direction = (target.position - arrow.transform.position).normalized;

            // 화살의 회전 설정
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            while (Vector3.Distance(arrow.transform.position, target.position) > 0.1f)
            {
                // 투사체 이동
                arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, target.position, (10f * atkSpd) * Time.deltaTime);
               
                yield return null; // 다음 프레임까지 대기
            }
            arrow.SetActive(false);
            Debug.Log("화살 쏨");
        }
        else
        {
            arrow.SetActive(false);
        }
      
    }

}