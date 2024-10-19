using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    
    CellScript[,] grid;
    float spacing = 1.1f;

    float simulationTimer;
    float simulationRate = 0.1f;

    public TMP_Text scoreText;
    public int score = 0;

    //to Instantiate coin objects within the grid of cells

    public GameObject coinPrefab;
    CoinScript[,] coinGrid;

    // Start is called before the first frame update
    void Start()
    {
        simulationTimer = simulationRate;

        // Instantiate a grid of cells
        grid = new CellScript[20,20];
        coinGrid = new CoinScript[20,20];

        for (int x = 0; x < 20; x++) {
            for (int y = 0; y < 20; y++) {
                // Use the x, y interator variables to compute each cell's position. If this is confusing
                // to you, try thinking about instantiating a row of cells rather than a grid, and using
                // the iterator variable of a single for loop to compute the position.
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing;
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                
                grid[x,y] = cell.GetComponent<CellScript>();
                grid[x,y].alive = (Random.value > 0.5f); // Assign random true or false to the alive of the cell.
                
                grid[x,y].xIndex = x;
                grid[x,y].yIndex = y;
            }
        }
        AssignCoins(10);
    }

    public int CountNeighbors(int xIndex, int yIndex)
    {
        int count = 0;     

        for (int x = xIndex - 1; x <= xIndex + 1; x++)
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {
                if (x >= 0 && x < 20 && y >= 0 && y < 20)
                {
                    if (!(x == xIndex && y == yIndex))
                    {
                        if(grid[x,y].alive)
                        {
                            count++;
                        }
                    }
                }
            }
        }
        return count;
    }

    // Update is called once per frame
    void Update()
    {
        simulationTimer -= Time.deltaTime;
        
        //Simulate until player has collected 10 coins(score=10)
        //while(score < 10)
        if (Input.GetKey(KeyCode.Space))
        {
            Simulate();
            simulationTimer = simulationRate;
        }
    }

    void AssignCoins(int coinCount)
    {
        int coinsAssigned = 0;

        while (coinsAssigned < coinCount)
        {
            //pick x and y coordinates based on grid size
            int randX = Random.Range(0,20);
            int randY = Random.Range(0,20);

            //if cell is alive and does not have a coin then place a coin above cell
            //for some reason I can't seem to get the coins to move with the cells
            if (grid[randX, randY].alive && coinGrid[randX, randY] == null)
            {
                Vector3 coinPos = grid[randX, randY].transform.position;
                coinPos.y += 2.0f;

                GameObject coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);

                coin.transform.parent = grid[randX, randY].transform;

                coinGrid[randX, randY] = coin.GetComponent<CoinScript>();

                coinsAssigned++;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //supposoed to destroy and add to the score but it isn't working
        if (other.CompareTag("coin"))
        {
            score++;

            Destroy(other.gameObject);

            scoreText.text = "Score: " + score;
        }
    }

    void Simulate()
    {
        bool[,] nextAlive = new bool[20,20];
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                int neighborCount = CountNeighbors(x,y);
                if (grid[x,y].alive && neighborCount < 2)
                {
                    //underpopulation 
                    nextAlive[x,y] = false;
                }
                else if (grid[x,y].alive && (neighborCount == 2 || neighborCount == 3))
                {
                    //healthy community
                    nextAlive[x,y] = true;
                }
                else if (grid[x,y].alive && neighborCount > 3)
                {
                    //overpopulation
                    nextAlive[x,y] = false;
                }
                else if (!grid[x,y].alive && neighborCount == 3)
                {
                    //reproduction
                    nextAlive[x,y] = true;
                }
                else
                {
                    nextAlive[x,y] = grid[x,y].alive;
                }
            }
        }
        //iterate through the grid and make changes to heights of cells depending on the alive status
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                grid[x,y].alive = nextAlive[x,y];
                
                //raise the cell up if it is alive
                if (grid[x,y].alive && grid[x,y].gameObject.transform.localScale.y < 10)
                {
                    grid[x,y].aliveCount++;
                    grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, grid[x,y].gameObject.transform.localScale.y + .5f, grid[x,y].gameObject.transform.localScale.z);

                    //change the position of the coin depending on the amount raised for the cell
                    if (coinGrid[x,y] != null)
                    {
                        Vector3 coinPos = coinGrid[x,y].transform.position;
                        coinPos.y = grid[x,y].transform.position.y + 1.0f;
                        coinGrid[x,y].transform.position = coinPos;
                    }
                }
                //lower the cell down if it is not alive
                else if (!grid[x,y].alive && grid[x,y].gameObject.transform.localScale.y > 1)
                {
                    grid[x,y].aliveCount++;
                    grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, grid[x,y].gameObject.transform.localScale.y - .5f, grid[x,y].gameObject.transform.localScale.z);

                    //change the position of the coin depending on the amount raised for the cell
                    if (coinGrid[x,y] != null)
                    {
                        Vector3 coinPos = coinGrid[x,y].transform.position;
                        coinPos.y = grid[x,y].transform.position.y + 2.0f;
                        coinGrid[x,y].transform.position = coinPos;
                    }
                }

                grid[x,y].SetColor();
            }
        }
    }
}
