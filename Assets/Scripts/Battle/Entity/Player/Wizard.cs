using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Wizard : Ally
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("Wizard 생성");
        job = JobClass.Wizard;
    }

    /*protected override void Update()
    {
        base.Update();
        *//*if (_curstate == State.Skill)
        {
            Skill();
        }*//*
        //cur_target = target;
    }*/


    /*public void Skill()
    {
        if (_curstate == State.Skill)
        {
            
            StopAllCoroutines();
            if (isAttack)
            {
                
                BaseEntity target = FindTarget().GetComponent<BaseEntity>();
                Debug.Log("타겟의 적에게 2배의 데미지로 한번 공격" + " " + (atkDmg * 2) + "데미지");
                target.cur_Hp -= atkDmg * 2;
                Debug.Log(target.cur_Hp + " " + target.name);
            }
            else
            {
                return;
            }
            cur_Mp = 0;
            ChangeState(State.Idle);
        }
    }*/
}