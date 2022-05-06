using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public int descentTimer = 1;
    public float speedMove = 0.3f;
    public float mouseSpeed = 1f;
    public float jumpSpeed = 10f;
    // Decent number for gravmulti is 0.2f with a drag of 0.75
    public float gravMultiplier = 0.5f;
    public float airMovementReducer = 0.5f;
    private bool ascent = false;
    private bool onGround = true;
    // private Vector2 rotation;
    private Vector3 moveDirection = Vector3.zero;
    public Rigidbody rb;
    public Animator animator;
    //Rotate Player facing direction
    public bool faceRight = true;




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug stuff");

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.zero;

       /* // unneeded, used for 3d movement
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;

        }
        // unneeded, uesd for 3d movement
        else if(Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;

        }*/
        // moves the character to the right
        if(Input.GetKey(KeyCode.D))
        {
            //moveDirection += transform.right;
            //New Move Below
            //When facing left and move right, rotate to face right
            if (faceRight == false)
            {
                transform.Rotate(0, 180f, 0, Space.Self);
                faceRight = true;
            } else
            {
                moveDirection += transform.right;
            }
        }
        // moves the character to the left
        else if(Input.GetKey(KeyCode.A))
        {
            //moveDirection -= transform.right;
            //New Move Below
            //When facing right and move left, rotate to face left
            if (faceRight == true)
            {
                transform.Rotate(0, 180f, 0, Space.Self);
                faceRight = false;
            } else
            {
                moveDirection += transform.right;
            }
        }
        // Alex 3/13/2022
        // Currently need to fix the jump as it is not working in this project or my test project.
        // why is jumping so complex ,_,
        // And now it is working for NO REASON
        // Alex 3/20/2022
        // temp turning off the ground check, it works in 3d but not 2d, need to fix it.
        // also to the #f changes the ray distance. this can fix some issues (such as the player is rather tall and we need the 
        // ray to actually hit
        if(Input.GetKeyDown(KeyCode.Space) && ascent == false /*&& onGround == true*/)
        {
            Debug.DrawRay(transform.position, -Vector3.up * 4f, Color.red, 3);
            if(Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 4f))
            {
                Debug.Log("Ray has hit " + hit.collider.gameObject.name);
                onGround = false;
                ascent = true;
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);

                // Invoke calls a method after the given number of seconds have passed
                Invoke("Descent", descentTimer);

                // subtract from y axis while in air to act as increased gravity
                // set a boolean when in the air
            }
            else
            {
                Debug.Log("The ray did not hit");
            }


        }
        //Alex 3/20/2022
        //Commenting out some stuff related to onGround till i get it fixed, means air movement will be a bit... powerful for now
        if (onGround == false)
        {
            moveDirection *= airMovementReducer;

            if (ascent == false)
            {
                moveDirection -= transform.up * gravMultiplier;
            }

        }
        rb.MovePosition(transform.position + moveDirection * speedMove);

        // Set ground to on by default, when the player jumps set ground to false and prepare to detect if the player has entered
        // collision




        //would want to move to another script, probably called camera
        //rotation += new Vector2(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"));
        //Camera.main.transform.localRotation = Quaternion.Euler(-rotation * mouseSpeed);

        //Animation Script
        //First two ifs remove error messages
        if (animator != null)
        {
            if (animator.runtimeAnimatorController != null)
            {
                //If the character is moving, movement animation becomes true
                if (Input.GetKey(KeyCode.D))
                {
                    animator.SetBool("isMoving", true);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }

                // Will be added when jump animation is added
                // else if (rb.velocity.z != 0)
                // {
                // animator.SetBool("jump", true);
                // }
            }

        }



    }

    //activates about every 4 frames or when physics updates
    void FixedUpdate()
    {

    }

    // a method that turns off ascent
    void Descent()
    {
        ascent = false;
        Debug.Log("Descent has been called.");

    }

    // modifies OnCollisionEnter to output what it collided with and change onGround to true
    void OnCollisionEnter(Collision c)
    {
    // collision is the information about the collision event
    // collider is the actual colliding collider
        Debug.Log(gameObject.name + " collided with " + c.collider.gameObject.name);
        onGround = true;
    }
}
