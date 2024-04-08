using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : BaseEntity
{
    // 플레이어 오브젝트에는 어떤 행동을 하는지 설정 ( 추후 Player로 말고 BaseEntity를 상속받는 각각 직업마다 클래스를 만들 예정 )
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

        _curstate = State.Idle;
        Debug.Log("Player의 Idle 상태");
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
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
                case State.Death:
                    Destroy(gameObject);
                    break;
            }

            _stateManager.UpdateState();


            if (cur_Hp <= 0)
            {
                _curstate = State.Death;
            }

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
