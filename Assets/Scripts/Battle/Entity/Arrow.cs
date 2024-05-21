using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private BaseEntity shooter;
    private BaseEntity target;
    public float speed = 10f;
    
    public void Shoot(BaseEntity shooter, BaseEntity target) 
    {
        this.shooter = shooter;
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            // TODO : 이동
            // Todo : 타겟 위치와 내 위치에 따른 Angle 조정

            Vector2 direction = (target.transform.position - transform.position).normalized;
            transform.right = direction;

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEntity hiter = collision.GetComponent<BaseEntity>();
        if (target == hiter)
        {
            // TODO : 애니메이션 완성시 활성화 필요
            //enemy.ani.SetTrigger("isHit");

            shooter.RangeHit(hiter);

            this.gameObject.SetActive(false);
        }
    }
}
