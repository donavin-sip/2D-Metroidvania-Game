using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Use Tags to refer to particular sets of objects

public class Health : MonoBehaviour
{
    public int max_health;      // max hp
    public int curr_health;     // current hp

    void Start()
    {
        if(this.gameObject.tag == "Player")
        {
            max_health = 5;
            curr_health = 5;
        }
        else if(this.gameObject.tag == "Enemy")
        {
            max_health = 3;
            curr_health = 3;
        }
    }

    void Update()   // Update is called once per frame
    {
        //if(curr_health <= 0)
        //{
        //    Destroy(this);
        //}
    }
}
