using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


    public float speed;
    private Rigidbody2D rb; //contains all the physicis in unity by default
    private Animator anim;

    public int health;

    private Vector2 moveAmount; //detect calculate how much we are going to move

    public Image[] hearts; //contains all hearts
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator cameraAnim;
    public Animator hurtAnim;

    private SceneTransitions sceneTransitions;

    public GameObject soundObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); //setting rb var = to the component attached to player character
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //x y coordinates (x,y) (x,1) for up (x, -1) down etc.
        moveAmount = moveInput.normalized * speed; //.normalized doesnt move faster when moving diagonally

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true); //we want to transition from idle to run 
        }
        else
        {
            anim.SetBool("isRunning", false); //transitions to idle animations
        }
    }


    private void FixedUpdate() //any physics call are here
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime); //move same speed 
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Instantiate(soundObject, transform.position, transform.rotation);
        cameraAnim.SetTrigger("shake");
        hurtAnim.SetTrigger("hurt");
        UpdateHealthUI(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }


    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i<hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if(health+healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
