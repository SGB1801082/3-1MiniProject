using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public BaseEntity target;
    public float speed = 10f;
    public bool hitcheck = false;

    private void Update()
    {
        if (target != null)
        {
            /*Vector2 direction = (target.transform.position - transform.position).normalized;
            transform.up = direction;*/

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        hitcheck = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == collision.GetComponent<BaseEntity>())
        {
            BaseEntity enemy = collision.GetComponent<BaseEntity>();

            //enemy.ani.SetTrigger("Hit_Arrow");
            hitcheck = true;
            this.gameObject.SetActive(false);
        }
    }
}
