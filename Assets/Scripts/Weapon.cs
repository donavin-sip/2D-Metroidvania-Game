using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Read the mouse click and calculate the rotation between the player and the click, then apply it to the firepoint which should fire the weapon properly

public class Weapon : MonoBehaviour
{
    public Transform firePoint;         // point at which the weapon fires
    public Transform targetPoint;       // pivot point for rotating by mouse
    public GameObject weaponPrefab;     // the weapon being produced for replication

    public float speed = 2000.0f;        // speed at which the weapon fires
    public float baseAngle = 90.0f;     // base angle from the player, 90 degrees
    
    Vector3 wepAngles = new Vector3(90.0f, 90.0f, 0.0f);        // sets the angle of the weapon from 0, 0, 0 degrees
    Quaternion wepRotation;                                     // Rotation of Weapon
    Quaternion fpRotation;                                      // Rotation of Fire Point

    Vector3 mousePos;               // Position of the mouse
    Vector3 pointPos;               // Position of the pivot
    float pointAngle;               // Angle stored for affecting pivot

    public GameObject tempWeapon = null;       // cloned object variable
    float weaponLife = 5.0f;                   // time before despawn

    void Update()   // Update is called once per frame
    {
        // Aiming weapon using mouse: Rotate the fire point pivot
        mousePos = Input.mousePosition;                                             // grabs mouse position as input
        mousePos.z = 40.0f;                                                         // Camera Z distance away
        pointPos = Camera.main.WorldToScreenPoint(targetPoint.position);            // gets the position of the target
        mousePos.x = mousePos.x - pointPos.x;                                       // X distance difference
        mousePos.y = mousePos.y - pointPos.y;                                       // Y distance difference
        pointAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;           // Calculates the angle for the target
        targetPoint.rotation = Quaternion.Euler(new Vector3(0, 0, pointAngle));     // sets the angle on the Z-axis for rotation on the pivot

        // 4/10/2022 update: wepAngles and pointAngle constraints were added under the old weapon prefab, if un-needed can remove
        if (Input.GetButtonDown("Fire1"))        // Fire1 is set to left-mouse click (range designed for current fire point distance from player)
        {
            wepAngles.x = baseAngle - pointAngle;       // gives the angle of the firing to the weapon w/ respect to the player (90 degrees flat fire)
            wepRotation.eulerAngles = wepAngles;        // orientation of the object
            Fire(wepAngles.x);                          // Calls the fire function

            // clear cloned objects
            //Destroy(GameObject.Find("Weapon_temp(Clone)"), 1);
            Destroy(tempWeapon, weaponLife);
        }
    }

    void Fire(float weaponAngle)     // weapon fire logic
    {
        GameObject instFire = Instantiate(weaponPrefab, transform.position, wepRotation) as GameObject;     // Instantiates the object (object and orientation)
        Rigidbody instFireRB = instFire.GetComponent<Rigidbody>();                                          // Sets the Rigidbody
		Vector3 parentPosition = transform.parent.position;													// gets the parent's position
		Vector3 direction = (transform.position - parentPosition).normalized;								// creates a direction vector for rotating the projectile and firing it in the specified direction
		instFire.transform.LookAt(transform.position + direction);											// rotates the created projectile
        instFireRB.AddForce(direction * speed);                                         					// Propels object up & right from firePoint //Alex 4/7/2022: edited to fire in the direction the firepoint is facing

        // code for original firepoint and weapon prefab
        //if (weaponAngle < (10.0f + baseAngle) && weaponAngle > (-10.0f + baseAngle))    // forward fire
        //{
        //    instFireRB.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * speed);         // Propels object right from firePoint (up removed w/ addition of angled shooting)
        //}
        //else if(weaponAngle <= (-10.0f + baseAngle))  // upwards fire
        //{
        //    instFireRB.AddForce(new Vector3(1.0f, 1.0f, 0.0f) * (speed - 300.0f));      // Propels object right and up from firePoint, reduce speed for diagonal travel
        //    //Debug.Log(weaponAngle + " upwards");
        //}
        //else if(weaponAngle >= (10.0f + baseAngle))   // downwards fire
        //{
        //    instFireRB.AddForce(new Vector3(1.0f, -1.0f, 0.0f) * (speed - 300.0f));     // Propels object right and down from firePoint, reduce speed for diagonal travel
        //    //Debug.Log(weaponAngle + " downwards");
        //}

        tempWeapon = instFire;

        // should create a delay in order to avoid high fire rate, call coroutine function
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
