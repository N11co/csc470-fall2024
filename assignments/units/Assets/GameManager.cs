using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    
    public GameObject workerNPCPrefab;
    public Transform spawnPoint;

    
    public Action SpacebarPressed; //observer pattern: used to "listen" (maybe use unitIsClicked)   

    public static GameManager instance;    

    public UnitScript selectedUnit;

    //to keep track of selected units
    public List<UnitScript> units = new List<UnitScript>();
    public Camera mainCamera;    
    public GameObject popUpWindow;


    LayerMask layerMask;
    bool isOver = false;

    //text elements for screen
    public TMP_Text seaweedText;
    public TMP_Text moneyText;
    public TMP_Text crateAmtText;
    public TMP_Text WorkerCountText;
    public TMP_Text screenText;

    //variables to keep count of elements
    public int seaweedCount = 0;
    public int moneyAmount = 0; 
    public int workerTurtleCount = 0;
    public int crateSeaweedCount = 0;

    void OnEnable()
    {
        //Singleton
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
        layerMask = LayerMask.GetMask("ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            screenText.text = "You Win!";
            return;
        }

        if (workerTurtleCount == 5)
        {
            isOver = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacebarPressed.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
            {
                //if we get in here the mouse is over the ground
                if (selectedUnit != null)
                {
                    //selectedUnit.destination = hitInfo.point;
                    selectedUnit.nma.SetDestination(hitInfo.point);
                    //selectedUnit.nma.destination(hitInfo.point);

                }
            }
        }
    }


    public void AddSeaweedToCrate(int amnt)
    {
        crateSeaweedCount += amnt;
        crateAmtText.text = "Seaweed in crate: " + crateSeaweedCount;
        Debug.Log("Seaweed in crate collected. Total: " + crateSeaweedCount);
    }

    public void AddCrateToTurtle(int num)
    {
        seaweedCount += num;
        seaweedText.text = "Seaweed: " + seaweedCount;
        Debug.Log("Seaweed collected. Total: " + seaweedCount);
        crateSeaweedCount -= num;
        crateAmtText.text = "Seaweed in crate: " + crateSeaweedCount;
        Debug.Log("Seaweed in crate collected. Total: " + crateSeaweedCount);
    }

    public void AddSeaweed(int amount)
    {
        seaweedCount += amount;
        seaweedText.text = "Seaweed: " + seaweedCount;
        Debug.Log("Seaweed collected. Total: " + seaweedCount);
    }

    public void ConvertSeaweedToMoney()
    {
        if (seaweedCount >= 5)
        {
            seaweedCount -= 5;
            moneyAmount++;
            seaweedText.text = "Seaweed: " + seaweedCount;
            moneyText.text = "Money: " + moneyAmount;
            Debug.Log("Converted 20 seaweed to money. Total money: " + moneyAmount);
        }
    }

    public void SpawnWorkerTurtle()
    {
        if (moneyAmount >= 5)
        {
            if (workerNPCPrefab != null && spawnPoint != null)
            {
                GameObject newNPC = Instantiate(workerNPCPrefab, spawnPoint.position, spawnPoint.rotation);
                moneyAmount -= 5;
                workerTurtleCount++;
                WorkerCountText.text = "Workers: " + workerTurtleCount;
                moneyText.text = "Money: " + moneyAmount;
                Debug.Log("Spawned worker turtle. Total workers: " + workerTurtleCount);
                //buyButtonClicked();
            }
        }
    }

    //public void OpenCharacterSheet(UnitScript unit)
    public void OpenInfoSheet()
    {
        if (selectedUnit == null) return;

        popUpWindow.SetActive(true);
    }

    public void SelectUnit(UnitScript unit)
    {
        //deselect all other units
        foreach(UnitScript u in units)
        {
            u.selected = false;
            u.bodyRenderer.material.color = u.normalColor;
        }

        //select the new unit
        selectedUnit = unit;

        unit.selected = true;

        //runs this on the selected unit once true
        unit.bodyRenderer.material.color = unit.selectedColor;
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
