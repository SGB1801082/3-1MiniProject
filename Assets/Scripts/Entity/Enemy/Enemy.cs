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
    private StateManager _stateManager;


    private void Start()
    {
        Debug.Log("Enemy의 Start 실행됨");

        _curstate = State.Idle;
        Debug.Log("Enemy의 Idle 상태");
        _stateManager = new StateManager(new IdleState(this));

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
                        if (IsAttack())
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
                        if (IsAttack())
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
                        if (!IsAttack())
                        {
                            ChangeState(State.Move);
                        }
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
            }

            _stateManager.UpdateState();
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
