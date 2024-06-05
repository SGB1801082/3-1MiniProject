using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();

        InitStat(50, 0, 5, 1, 1.1f);
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
