using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    public float stopDistance;

    private float attackTime;

    private Animator anim;

    public Transform shotPoint;

    public GameObject enemyBullet;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if(Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //convert direction to angle, pass direction x and y and times the value to create degrees instead of radians
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); //quaternion is unity rotation, convert our angle to pass in our function, -90 gives us a better roation mouse following
        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
