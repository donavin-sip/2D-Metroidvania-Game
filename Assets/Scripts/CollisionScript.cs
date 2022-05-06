using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private Health healthController;        // references the Health script
    private Collider objCollider;           // refers to the object's collider in which the script is attached to

    bool isColliding = false;

    void Awake()
    {
        healthController = this.GetComponent<Health>();     // grabs the Health script
    }

    void Start()    // Start is called before the first frame update
    {
        objCollider = GetComponent<Collider>();             // grabs the object's collider
    }

    void Update()   // Update is called once per frame
    {
        isColliding = false;

        if(healthController.curr_health <= 0)       // if current hp is 0 or below, the object is destroyed
        {
            Debug.Log("The object has been destroyed.");
            Destroy(this);
            objCollider.enabled = !objCollider.enabled;     // turns off collider
        }
    }

    void OnCollisionEnter(Collision collision)      // upon collision, notify collision and decrement health
    {
        Debug.Log(collision.gameObject.name + " has collided with " + this.name);
        //if(collision.gameObject.tag == "Weapon" && isColliding == false)
        //{
        //    isColliding = true;
        //    healthController.curr_health -= 1;
        //    //Destroy(collision.gameObject.GetComponent<Weapon>().tempWeapon);      // begins nullpoint reference warning
        //}
        Debug.Log(healthController.curr_health);
    }

    void OnTriggerEnter(Collider otherObj)
    {
        Debug.Log(otherObj.gameObject.name + " has collided with " +	 this.name);
        if(otherObj.gameObject.tag == "Weapon")
        {
            if(isColliding == false)
            {
                healthController.curr_health -= 1;
                //yield return new WaitForSeconds(1);   // IEnumerator
                isColliding = true;
                //triggerCoroutine();
				Debug.Log(healthController.curr_health);
            }
        }

        
    }

    IEnumerator triggerCoroutine()
    {
        yield return new WaitForSeconds(3);
        isColliding = true;
    }
}
