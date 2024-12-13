using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public Action SpaceBarPressed;    

    public static GameManager instance;    

    public UnitScript selectedUnit;

    //public NavMeshAgent nma;

    public List<UnitScript> units = new List<UnitScript>();

    public Camera mainCamera;    

    public GameObject popUpWindow;

    //public TMP_Text objectiveText;

    public TMP_Text nameText;

    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    LayerMask layerMask;

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
        layerMask = LayerMask.GetMask("ground");
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
        }
    }

    public void OpenCharacterSheet(UnitScript unit)
    {
        if (selectedUnit == null) return;
    
        //objectiveText.text = unit.objective;
        nameText.text = unit.unitName;
        bioText.text = unit.bio;
        statText.text = unit.stats;

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
        unit.bodyRenderer.material.color = unit.selectedColor;
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
