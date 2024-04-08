using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(BaseEntity e) : base(e) { }

    public override void OnStateEnter()
    {
        entity.StopMove();
        entity.StartCoroutine(entity.Attack());
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
        entity.StopCoroutine(entity.Attack());
    }
}
