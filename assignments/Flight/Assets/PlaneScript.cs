using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    public GameObject cameraObject;    

    public Terrain terrain;

    public TMP_Text scoreText;

    public TMP_Text ScreenText;

    public TMP_Text timerText;

    public TMP_Text timerRunOutText;

    int score = 0;

    float timer = 45.0f;

    bool isDead = false;

    // This will control how the plane moves
    float forwardSpeed = 12f; //* Time.deltaTime; //12f
    float deaccelerationRate = 0.5f; 
    float xRotationSpeed = 90f; //* Time.deltaTime;  //90f
    float yRotationSpeed = 90f; //* Time.deltaTime;  //90f


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float terrainHeight = terrain.SampleHeight(transform.position);    

        if (isDead)
        {
            return;
        }

        //timer and score outcomes
        if ((timer <= 0 && score < 9)) 
        {
            timerRunOutText.text = "You ran out of time!";
            
            isDead = true;

            float timerStop = timer; 

            timerStop = Mathf.Round(timerStop * 100f) / 100f;
            timerText.text = "Timer: " + timerStop + " secs";
            
            forwardSpeed = 0f;
            xRotationSpeed = 0f;
            yRotationSpeed = 0f;
            
            transform.position = new Vector3(0,25,0);
        }
        else if ((timer > 0 && score == 9) && (transform.position.y > terrainHeight))
        {
            ScreenText.text = "You Win!";
            
            isDead = true;
            
            float timerStop = timer; 

            //timer = timerStop;
            timerStop = Mathf.Round(timerStop * 100f) / 100f;
            timerText.text = "Timer: " + timerStop + " secs";
            
            forwardSpeed = 0f;
            xRotationSpeed = 0f; 
            yRotationSpeed = 0f;
            
            transform.position = new Vector3(0,25,0);
        }


        //Timer to let the user know how much time left to collect all coins
        //If user does not get all the coins within the timeframe they lose
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
            timer = Mathf.Round(timer * 100f) / 100f;
        }
        else
        {
            timer = 0f;
        }

        timerText.text = "Timer: " + timer + " secs";

        //Make the plane move forward but slowing down
        forwardSpeed -= deaccelerationRate * Time.deltaTime;
        
        if (forwardSpeed < 0 && score != 9){
            isDead = true;    

            forwardSpeed = 0f;
            xRotationSpeed = 0f; 
            yRotationSpeed = 0f;
            
            ScreenText.text = "You died.";
            transform.position = new Vector3(0,25,0);

            timer = 0f;
        }

        // Make the plane move forward by adding the forward vector to the position
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;    
        
        // Get directional input (up, down, left, right)
        float hAxis = Input.GetAxis("Horizontal"); //-1 if left is pressed, 1 if right is pressed, 0 if neither is pressed
        float vAxis = Input.GetAxis("Vertical"); //-1 if down is pressed, 1 if up is pressed, 0 if neither is pressed

        // Apply rotation based on the inputs
        Vector3 amountToRotate = new Vector3(0,0,0);
        amountToRotate.x = vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed;
        amountToRotate *= Time.deltaTime; //amountToRotate = amountToRotate * Time.deltaTime
        transform.Rotate(amountToRotate, Space.Self);

        // Deal with colliding with terrain 
        if (transform.position.y < terrainHeight)
        {
            
            isDead = true;
        
            forwardSpeed = 0f;
            xRotationSpeed = 0f; 
            yRotationSpeed = 0f;
            
            transform.Rotate(0,0,0, Space.World);

            float timerStop = timer; 

            timerStop = Mathf.Round(timerStop * 100f) / 100f;
            timerText.text = "Timer: " + timerStop + " secs";
            
            ScreenText.text = "You died.";
            transform.position = new Vector3(0,25,0);
        }

        // Position the camera 
        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 10f;
        cameraPosition += Vector3.up * 8f;
        cameraObject.transform.position = cameraPosition;

        cameraObject.transform.LookAt(transform.position);
    }
    
    // Unity will tell the function below to run under the following conditions:
    //  1. Two objects with colliders are colliding
    //  2. At least one of the objects' colliders are marked as "Is Trigger"
    //  3. At least one of the objects has a Rigidbody
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        // 'other' is the name of the collider that just collided with the object
        // that this script is attached to (the plane).
        // Check to see that it has the tag "collectable". Tags are assigned in the Unity editor.
        if (other.CompareTag("collectable"))
        {
            score++;

            Destroy(other.gameObject);

            scoreText.text = "Score: " + score;

            float maxSpeed = 25f;

            forwardSpeed += 12f;

            if (maxSpeed < forwardSpeed)
            {
                forwardSpeed = maxSpeed;
            }
        }
        else if (other.CompareTag("wall"))
        {
            transform.Rotate(0,180,0, Space.World);
        }
        else if (other.CompareTag("gorilla"))
        {
            isDead = true;
            ScreenText.text = "You died.";
            forwardSpeed = 0f;
            xRotationSpeed = 0f; 
            yRotationSpeed = 0f;
            transform.position = new Vector3(0,25,0);
        }
    }
}
