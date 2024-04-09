using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{
    // 적 오브젝트 행동 클래스 ( 추후 BaseEntity를 상속받는 적들을 구분해서 클래스를 만들 예정 )
    public enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Death
    }

    public State _curstate;
    public StateManager _stateManager;


    private void Start()
    {
        Debug.Log("Enemy의 Start 실행됨");

        _curstate = State.Idle;
        Debug.Log("Enemy의 Idle 상태");
        _stateManager = new StateManager(new IdleState(this));


        max_Hp = 10f;
        cur_Hp = max_Hp;
        max_Mp = 5f;
        cur_Mp = max_Mp;
        atkDmg = 1f;
        atkRange = 1f;
        atkSpd = 1f;

    }

    private void Update()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Battle)
        {
            switch (_curstate)
            {
                case State.Idle:
                    if (FindTarget() != null)
                    {
                        if (IsAttack(atkRange))
                        {
                            ChangeState(State.Attack);
                        }
                        else
                        {
                            ChangeState(State.Move);
                        }
                    }
                    break;

                case State.Move:
                    if (FindTarget() != null)
                    {
                        if (IsAttack(atkRange))
                        {
                            ChangeState(State.Attack);
                        }
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;

                case State.Attack:
                    if (FindTarget() != null)
                    {
                        if (!IsAttack(atkRange))
                        {
                            ChangeState(State.Move);
                        }

                        if (isAtkDone)
                        {
                            Debug.Log("공격 완료 - Idle로 상태 변경 (새로운 타겟 지정)");
                            isAtkDone = false;
                            ChangeState(State.Idle);
                        }
                    }
                    else
                    {
                        Debug.Log("새로운 타겟 없음");
                        isAtkDone = false;
                        ChangeState(State.Idle);
                    }
                    break;
                case State.Death:
                    Destroy(gameObject);
                    break;
            }

            _stateManager.UpdateState();

            /*if (cur_Hp <= 0)
            {
                _curstate = State.Death;
            }*/

        }
    }

    private void ChangeState(State newState)
    {
        _curstate = newState;

        switch (_curstate)
        {
            case State.Idle:
                _stateManager.ChangeState(new IdleState(this));
                break;
            case State.Move:
                _stateManager.ChangeState(new MoveState(this));
                break;
            case State.Attack:
                _stateManager.ChangeState(new AttackState(this));
                break;
        }
    }
}
