using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Player playerScript; //deal damage to player
    private Vector2 targetPosition;

    public float speed;

    public int damage;

    public GameObject effect;
    

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            //player.GetComponent<Player>().TakeDamage(damage);
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
