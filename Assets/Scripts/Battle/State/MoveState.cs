using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveState : BaseState
{

    public MoveState(BaseEntity e) : base(e) { }

    public override void OnStateEnter()
    {
        if (entity != null && entity.FindTarget() != null)
        {
            entity.MoveToTarget();
        }
    }

    public override void OnStateUpdate()
    {
        if (entity != null && entity.FindTarget() != null)
        {
            entity.MoveToTarget();
        }
    }

    public override void OnStateExit()
    {
        if (entity != null && entity.FindTarget() != null)
        {
            entity.StopCoroutine(entity.UpdateTarget());
        }
    }
}
