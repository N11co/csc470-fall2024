using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{

    public Renderer cubeRenderer;

    //public Renderer coinRenderer;

    public bool alive = false;

    public int aliveCount = 0;

    public int xIndex = -1;
    public int yIndex = -1;

    public Color aliveColor;
    public Color deadColor;

    GameManager gameManager;

    //public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetColor();

        GameObject gmObj = GameObject.Find("GameManager");
        gameManager = gmObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        alive = !alive;
        SetColor();

        // Count neighbors!
        int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        Debug.Log("(" + xIndex + "," + yIndex + "): " + neighborCount);
    }
    public void SetColor()
    {
        if (alive) {
            cubeRenderer.material.color = aliveColor;
        } else {
            cubeRenderer.material.color = deadColor;
        }    
        //cubeRenderer.material.color = Color.HSVToRGB(aliveCount / 100f, 0.6f, 1f);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            alive = !alive;
            SetColor();
            Debug.Log("stepped on: " + xIndex + " " + yIndex);
        }
    }
}
