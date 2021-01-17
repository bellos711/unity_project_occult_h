using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    //destroy after couple seconds
    public float lifeTime;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
