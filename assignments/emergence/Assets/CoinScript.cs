using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Renderer coinRenderer;

    public int xIndex = 0;
    public int yIndex = 0;

    GameManager gameManager;

    public int score = 0;

    public bool present = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObj = GameObject.Find("GameManager");
        gameManager = gmObj.GetComponent<GameManager>();

        SetPresent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPresent()
    {
        if (present)
        {
            coinRenderer.enabled = false;
        }
        else
        {
            coinRenderer.enabled = true;
        }
    }
}
