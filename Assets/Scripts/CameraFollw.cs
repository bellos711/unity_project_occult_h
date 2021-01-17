using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollw : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerTransform.position; //player and camera at same position
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform!=null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX); //
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY); //

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed); //smothly move from another point based on speed 
        }
    }
}
