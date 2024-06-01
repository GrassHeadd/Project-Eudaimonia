using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    //initialise each variables
    public float moveSpeed = 2.0f; // Speed at which the spider moves forward
    public float rotationSpeed = 100.0f; // Speed at which the spider rotates (not used in this script)
    public float changeDirectionTime = 3.0f; // Time interval before changing direction
    private float timerForDirectionChange;

    // Start is called before the first frame update
    void Start()
    {
        //start do nothing
        timerForDirectionChange = changeDirectionTime;
    }

    /*
        TO-DO: 
        -----------------------
        -- Basics to be done --
        -----------------------
        1. encapsulated within a certain region
        2. if sees a wall either go up on it or go another place
        3. occassional rest with only movement during very limited time (so the whole script has to change to such that depending on the random thing generated -
        it will either move [occasionally] or it will stay put, once it starts it can't stop)
        ----------------------------------
        -- Higher Level with other NPCs --
        ----------------------------------
        ESSENTIALLY MOVEMENT INTERACTIONS WITH OTHER THINGS
        1. if sees humans will either
            a. run away 
            b. climb on it
            abstracted out so its random then if climb on it it may bite u 
        2. can be inherited since this is the base case and adjust the stats accordingly base on the type of spiders
        3. if sees insects depending on risk level might move away but add a random factorx
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        //this part is to create random movement first before any interactions, so it will just randomly roam around
        timerForDirectionChange -= Time.deltaTime;
        
        // Check if the timer has reached zero or less
        if (timerForDirectionChange <= 0)
        {
            // Change the spider's direction
            changeRandomDirection();

            // Reset the timer to the initial change direction time
            timerForDirectionChange = changeDirectionTime;
        }

        // Move the spider forward continuously
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
    }

    void changeRandomDirection() 
    {
        //generate random angle to change direction to 
        float randomYRotation = Random.Range(0f, 360f);

        // Apply the new rotation to the spider's transform
        transform.eulerAngles = new Vector3(0, randomYRotation, 0);
    }

    void turnLeft()
    {
        transform.eulerAngles = new Vector3(0, 90f, 0);
    }
}
