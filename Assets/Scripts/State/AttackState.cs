using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(BaseEntity e) : base(e) { }

    public override void OnStateEnter()
    {
        entity.StopMove();
        entity.Attack();
    }

    public override void OnStateUpdate()
    {
/*        if(entity.FindTarget() != null && entity != null)
        {
*//*            if(entity.IsAttack())
            {

            }*//*
        }*/
        
    }

    public override void OnStateExit()
    {
    }
}
