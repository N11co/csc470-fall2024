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
    public PlatformerController player;

    public static GameManager instance;
    
    //change size of turtle(player)
    public Transform playerTransform;
    
    public int lives = 3;  //maybe 3 lives? 
    public int score = 0;
    public int dashCount = 1;

    public TMP_Text controlInfoText;
    public TMP_Text scoreText;
    public TMP_Text screenText;
    public TMP_Text livesCountText;
    public TMP_Text dashCountText;

    bool isDead = false;

    public GameObject popUpWindow;

    //to store last known player position
    public Vector3 lastPlayerPos;

    
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

    //update player's last known position
    public void UpdatePlayerPos(Vector3 pos)
    {
        lastPlayerPos = pos;
    }

    public Vector3 GetLastPlayerPos()
    {
        return lastPlayerPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting lives: " + lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            screenText.text = "You Died!";
            return;
        }
    }

    //add score when turtle player collides with seaweed objects
    public void AddScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
        Debug.Log("Seaweed collected. Total: " + score);

        //add dash after every 3 seaweed collected
        if (score % 3 == 0)
        {
            dashCount++;
        }

        dashCountText.text = "Dash: " + dashCount;

        if (score % 5 == 0)
        {
            Vector3 newScale = playerTransform.localScale * 1.25f;
            playerTransform.localScale = newScale;

            if (player != null)
            {
                player.IncreaseSpeed(2f);
            }

        }

        //once 15 collected end game
        if (score >= 15)
        {
            EndGame();
        }
    }

    public void OpenInfoSheet()
    {
        popUpWindow.SetActive(true);
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }

    //take away a life when turtle collides with enemy NPCs 
    public void DamageToPlayer(int damage)
    {
        lives -= damage;
        livesCountText.text = "Lives: " + lives;
        Debug.Log("Collided into enemy. Total lives: " + lives);
    }

    public void AddDash(int amt)
    {
        dashCount += amt;
        dashCountText.text = "Dash: " + dashCount;
        //Debug.Log("Collided into enemy. Total projectiles: " + projectileCount);
    }

    public void EndGame()
    {
        screenText.text = "You Win!";
        Time.timeScale = 0;
    }
}
