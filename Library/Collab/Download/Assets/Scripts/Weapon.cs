using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;         // point at which the weapon fires
    public GameObject weaponPrefab;     // the weapon being produced for replication

    public float speed = 500.0f;        // speed at which the weapon fires
    
    Vector3 angles = new Vector3(90.0f, 90.0f, 90.0f);      // sets the angle of the weapon from 0, 0, 0 degrees
    Quaternion rotation;

    GameObject tempWeapon = null;       // cloned object variable
    float weaponLife = 3.0f;            // time before despawn

    void Update()   // Update is called once per frame
    {
        if(Input.GetButtonDown("Fire1"))        // Fire1 is set to left-mouse click
        {
            rotation.eulerAngles = angles;      // orientation of the object
            transform.rotation = rotation;
            Fire();                             // Calls the fire function

            // clear cloned objects
            //Destroy(GameObject.Find("Weapon_temp(Clone)"), 1);
            Destroy(tempWeapon, weaponLife);
        }
    }

    void Fire()     // weapon fire logic
    {
        GameObject instFire = Instantiate(weaponPrefab, transform.position, rotation) as GameObject;    // Instantiates the object (object and orientation)
        Rigidbody instFireRB = instFire.GetComponent<Rigidbody>();                                      // Sets the Rigidbody
        instFireRB.AddForce(Vector3.forward * speed);                                                   // Propels object forward from firePoint

        tempWeapon = instFire;

        // should create a delay in order to avoid high fire rate
    }

    // NOTE: OnTrigger is better for out-of-bounds detection or expanding colliders, possible to implement where object is destroyed upon entering opposing object's space
    //void OnTriggerEnter(Collider hitInfo)     // hit detection
    //{
    //    Debug.Log(hitInfo.name);        // prints what the weapon has collided with
    //    Destroy(tempWeapon);            // instantly destroy object upon collision
    //}

    void OnCollisionEnter(Collision collision)  // hit detection, bounces off object
    {
        Debug.Log("Weapon collided with: " + collision.gameObject.name);    // prints what the weapon has collided with
        Destroy(tempWeapon);                                                // instantly destroy object upon collision
    }
    
}
