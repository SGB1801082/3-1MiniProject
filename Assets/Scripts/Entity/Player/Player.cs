using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : BaseEntity
{
    // 플레이어 오브젝트에는 어떤 행동을 하는지 설정 ( 추후 Player로 말고 BaseEntity를 상속받는 각각 직업마다 클래스를 만들 예정 )
    private enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Death
    }

    private State _curstate;
    private StateManager _stateManager;

    private void Start()
    {

        _curstate = State.Idle;
        Debug.Log("Player의 Idle 상태");
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

        if (gameObject != null)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
            }
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }
        }



    }

    private void ChangeState(State newState)
    {
        _curstate = newState;

        switch(_curstate) 
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
