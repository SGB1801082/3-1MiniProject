using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Start()
    {
        base.Start();
        // 최대 체력, 최대 마나, 공격력, 공격속도, 사거리, 근접유무, 스킬유무
        InitStat(15, 0, 1, 1, 6f, false, false);
    }


    public override void RangeAttack(BaseEntity target)
    {
        base.RangeAttack(target);
        GameObject obj_Arrow = BattleManager.Instance.pool.GetObject(1); // 돌 프리팹
        obj_Arrow.transform.position = transform.GetChild(0).position;
        Arrow arrow = obj_Arrow.GetComponent<Arrow>();
        arrow.Shoot(this, target);
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
