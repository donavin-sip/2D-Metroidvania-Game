using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBehavior : MonoBehaviour
{
	private GameObject player;
	//public GameObject target;
	public float movementTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
    player = GameObject.FindWithTag("Player");
		

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void HarpoonMovement()
	{

	}

	void OnCollisionEnter(Collision c)
	{
	    if(c.collider.CompareTag("Harpoonable"))
	    {
		    ContactPoint con = c.GetContact(0);
	        iTween.MoveTo(player, con.point, movementTime);
	        //c.collider.transform.position
	    }
	}
}
