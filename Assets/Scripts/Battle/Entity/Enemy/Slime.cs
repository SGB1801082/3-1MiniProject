using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void Start()
    {
        base.Start();

        InitStat(12, 0, 1, 1, 1.1f);
        isMelee = true;
        able_Skill = false;
    }

/*    protected override void Update()
    {
        base.Update();
        // 스킬 있으면 추가
    }

    public void Skill()
    {

    }*/
}
