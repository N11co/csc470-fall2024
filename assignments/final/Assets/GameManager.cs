using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//keep track of score, lives and changes in text on screen
//keep track of seafood collected
//collecting seaweeds gives you a projectile to shoot
//if shooting projectile collides with "crab" or "bird" destroy them
//if NPC projectile collides with player talke away one life
//if lives = 0 game over

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    
    //change size of turtle(player)
    public Transform playerTransform;
    
    public int lives = 3;  //maybe 3 lives? 
    public int score;
    public int projectileCount = 0;

    public TMP_Text controlInfoText;
    public TMP_Text scoreText;
    public TMP_Text screenText;
    public TMP_Text livesCountText;
    public TMP_Text projectileCountText;
    
    //singleton 
    void OnEnable()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        //if (other.CompareTag("MovingPlatform")) {
        //   MovingPlatform = other.gameObject;
        //    previousMovingPlatformPosition = MovingPlatform.transform.position;
        //}
        if (other.CompareTag("seaweed"))
        {
            score++;
            projectileCount++;
            //dashCount++;

            Destroy(other.gameObject);

            //dashCountText.text = "Dash Count: " + dashCount;
            scoreText.text = "Score: " + score;
            projectileCountText.text = "Projectiles: " + projectileCount;

            //check to see if score is 10, if so increase size of player(turtle)
            if (score == 10)
            {
                playerTransform.localScale *= 1.5f; 
            }
        }
        else if (other.CompareTag("bottleCap"))
        {
            score = score - 1;
            projectileCount = projectileCount - 1;
            Destroy(other.gameObject);
            scoreText.text = "Score: " + score;
            projectileCountText.text = "Projectiles: " + projectileCount;
            if (score < 0)
            {
                screenText.text = "You died.";
            }
        }
        else if ((other.CompareTag("crab")) || (other.CompareTag("bird")))
        {
            //isDead = true;
            lives = lives - 1;
            score = score - 1;
            if (score < 0)
            {
                screenText.text = "You died.";
            }
        }
    }

    //void OnTriggerExit(Collider other) {
    //   if (other.CompareTag("MovingPlatform")) {
    //        MovingPlatform = null;
    //    }
    //}
}
