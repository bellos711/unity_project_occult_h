using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;

    [HideInInspector] //don't show this transform player in unity inspector
    public Transform player;

    public float speed;

    public float timeBetweenAttacks;

    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    public int healthPickupChance;
    public GameObject healthPickup;

    public GameObject deathEffect;
    public GameObject blood;

    public GameObject soundObject;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //player equal to gameobject that has player type attached to ti
    }
    public void TakeDamage(int damageAmount)
    {
        Instantiate(soundObject, transform.position, transform.rotation);
        health -= damageAmount;
        

        if (health<=0)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            Instantiate(blood, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
