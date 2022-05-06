using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public string textValue;        // string of text
    public Text textElement;        // Text object
    private PlayerHealth healthShown;

    void Start()    // Start is called before the first frame update
    {
        textElement.text = textValue;   // assigns base text value onto the object
        healthShown = this.GetComponent<PlayerHealth>();         // grabs the Health script
    }

    void Update()   // Update is called once per frame
    {
        // update text for when player health changes
        textElement.text = healthShown.healthText;
    }
}
