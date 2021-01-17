using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;
    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position)>stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position; //position of melee enemy before jump
        Vector2 targetPosition = player.position;

        float percent = 0; //how much of animation we done so far 0 is start
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4; //enable us to go to target position and back to original position based on percent
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
