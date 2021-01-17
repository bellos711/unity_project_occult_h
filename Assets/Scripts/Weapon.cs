using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame

    //what to spawn? variable
    public GameObject projectile;

    //variable position we want to spawn projectile from
    public Transform shotPoint;
    public float timeBetweenShots; //time interval for our shot

    private float shotTime; //time before we can shoot another bullet
   


    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //subtract mouse position with transform position to get direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //convert direction to angle, pass direction x and y and times the value to create degrees instead of radians
        Quaternion rotation = Quaternion.AngleAxis(angle -90, Vector3.forward); //quaternion is unity rotation, convert our angle to pass in our function, -90 gives us a better roation mouse following
        transform.rotation = rotation;


        //if left mouse is pressed
        if(Input.GetMouseButton(0)) //0 for left mouse, 1 for right
        {
            if(Time.time >= shotTime) //if we're allowed to shoot
            {
                Instantiate(projectile, shotPoint.position, transform.rotation); //spawn our projectile at current position rotation of weapon
                shotTime = Time.time + timeBetweenShots;//recalculate the shot time set it to current time in game and adding into it timebetweenshots
            }

        }//endif mousebutton
    }
}
