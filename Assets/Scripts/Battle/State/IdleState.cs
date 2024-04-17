using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(BaseEntity e) : base(e) { }

    public override void OnStateEnter()
    {
        if (entity != null && entity.FindTarget() != null)
        {
            entity.StopMove();
            entity.ani.SetBool("isMove", false);
            entity.StartCoroutine(entity.UpdateTarget());
        }
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}
