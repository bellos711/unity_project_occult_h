using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    


    public float speed;
    public float lifeTime; //how long bullet stays in game before destroys

    public GameObject explosion;

    public int damage;

    public GameObject soundObject;
    void Start()
    {

        Invoke("DestroyProjectile", lifeTime); //call function but we need to write name of function between quotations
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); //move object in certain direction vector2.up is forward
    }


    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //first param what we spawning, 2nd param at what position, 3rd param quaternion, at what rotation
        Destroy(gameObject); //Destroy projectile oncelifetime is passed
    }

    private void OnTriggerEnter2D(Collider2D collision) //stores objects we collided with
    {
        if(collision.tag=="Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }

        if(collision.tag == "boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }//built in it gets called 
}
