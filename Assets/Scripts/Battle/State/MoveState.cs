using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveState : BaseState
{

    public MoveState(BaseEntity e) : base(e) { }

    public override void OnStateEnter()
    {
        entity.MoveToTarget();
    }

    public override void OnStateUpdate()
    {
        entity.MoveToTarget();
    }

    public override void OnStateExit()
    {
        entity.StopCoroutine(entity.UpdateTarget());
    }
}
