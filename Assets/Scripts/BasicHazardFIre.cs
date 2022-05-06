using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHazardFIre : MonoBehaviour
{


    public GameObject bulletPrefab;
    private GameObject tempBullet = null;
    
    public float baseAngle = 90.0f;
    public float bulletLife = 8.0f;
    public float speed = 800.0f;
	public float bulletFireSpeed = 2.0f;
	private bool isFiring = false;
	private bool hasBeenUnseen = false;
    public Vector3 direction = new Vector3(1, 0, 0);
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        GameObject instBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        Rigidbody instBulletRB = instBullet.GetComponent<Rigidbody>();

		

        instBulletRB.AddForce(direction * speed); 
        tempBullet = instBullet;

        Destroy(tempBullet, bulletLife);

    }


	void OnTriggerEnter(Collider triggeringObj)
	{
		Debug.Log(triggeringObj.gameObject.name + "has collided with the trigger");
		if(triggeringObj.gameObject.tag == "Player" && isFiring == false)
			{
				isFiring = true;
				InvokeRepeating("Fire", 0, bulletFireSpeed);
			}

		

	}
    void OnBecameVisible()
    {
	    if (hasBeenUnseen && isFiring == false)
	    {
		    isFiring = true;
		    hasBeenUnseen = false;
		    InvokeRepeating("Fire", 0, bulletFireSpeed);
		    
	    }
        Debug.Log("Became Visible");
    }

	//left in to prevent the game from becoming too laggy before I find a better method to cancel the repating invokes
    void OnBecameInvisible()
    {
	    hasBeenUnseen = true;
        CancelInvoke();
        isFiring = false;
        Debug.Log("Became Invisible");
    }
}
