using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Health healthController;       // references the Health script
    public TextScript playerHealthText;    // references the Text script for the player
    public string healthText;              // current text

    void Awake()
    {
        healthController = this.GetComponent<Health>();         // grabs the Health script
        playerHealthText = this.GetComponent<TextScript>();     // grabs the Text script
    }

    // Update is called once per frame
    void Update()
    {
        if(healthController.curr_health <= 0)   // player runs out of health
        {
            // do something like end the game or transition off
        }
    }

    void OnTriggerEnter(Collider enemyObj)
    {
        if(enemyObj.gameObject.tag == "Enemy")  // collision with enemies
        {
            Debug.Log(this.name + " has taken damage by " + enemyObj.gameObject.name);      // says which enemy has damaged the player
            healthController.curr_health -= 1;
            healthText = "Health: " + healthController.curr_health + " / " + healthController.max_health;       // writes new health for update
            playerHealthText.textValue = healthText;                                        // updates and passes
            //Debug.Log(healthText);

            // push out the player object to prevent no trigger while standing on top of enemy (or re-call OTE to decrement health)
        }

        //Debug.Log(this.name + "'s current hp: " + healthController.curr_health);
    }
}
