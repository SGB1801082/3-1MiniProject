using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{
    // �� ������Ʈ �ൿ Ŭ���� ( ���� BaseEntity�� ��ӹ޴� ������ �����ؼ� Ŭ������ ���� ���� )
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
        Debug.Log("Enemy�� Start �����");

        _curstate = State.Idle;
        Debug.Log("Enemy�� Idle ����");
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