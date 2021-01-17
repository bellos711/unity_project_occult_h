using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy //derive from enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition; //store selec ted area on map to spawn enemy
    private Animator anim; //to change animations

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float meleeAttackSpeed;
    public float stopDistance;
    private float attackTime;

    public override void Start() //override the start function in enemy
    {
        base.Start(); //call start from enemy script
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

        }
    }


    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position; //position of melee enemy before jump
        Vector2 targetPosition = player.position;

        float percent = 0; //how much of animation we done so far 0 is start
        while (percent <= 1)
        {
            percent += Time.deltaTime * meleeAttackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4; //enable us to go to target position and back to original position based on percent
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }


}
