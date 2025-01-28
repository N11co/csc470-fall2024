using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{

    //used for AI navigation
    public NavMeshAgent nma;
    public Vector3 destination;
    LayerMask layerMask;

    //To get render of unit body (used to change color) 
    public Renderer bodyRenderer;

    //bool to indicate whether selected or not
    public bool selected = false;

    //Color of unit when not selected
    public Color normalColor;

    //Color of unit when selected
    public Color selectedColor;

    //rotate speed of units when in place 
    float rotateSpeed;

    public Coroutine seaweedCollectionCoroutine;
    public Coroutine seaweedSellCoroutine;


    //subscribes to a specific event(s)
    void OnEnable()
    {
        GameManager.instance.SpacebarPressed += ChangeToRandomColor; //store the function to the right of the += in a list of functions to call when that action happens
    }

    //to unsubscribe to event(s)
    void OnDisable()
    {
        GameManager.instance.SpacebarPressed -= ChangeToRandomColor;
    }

    void ChangeToRandomColor()
    {
        bodyRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("unit");
    
        GameManager.instance.units.Add(this);

        rotateSpeed = Random.Range(20,60);
    }

    void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("seaweedFarm"))
        {
            if (seaweedCollectionCoroutine == null)
            {
                seaweedCollectionCoroutine = StartCoroutine(CollectSeaweedOverTime());
            }
            
        }
        if (other.CompareTag("seaweedShop") && GameManager.instance.seaweedCount >= 5)
        {
            if (seaweedSellCoroutine == null)
            {
                seaweedSellCoroutine = StartCoroutine(SellSeaweedOverTime());
            }
            
        }
        if (other.CompareTag("eggCenter") && GameManager.instance.moneyAmount >= 5)
        {
            GameManager.instance.SpawnWorkerTurtle();
        }
        if (other.CompareTag("crateCenter"))
        {
            GameManager.instance.AddCrateToTurtle(GameManager.instance.crateSeaweedCount);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("seaweedFarm"))
        {
            if (seaweedCollectionCoroutine != null)
            {
                StopCoroutine(seaweedCollectionCoroutine);
                seaweedCollectionCoroutine = null;
            }
        }
        if (other.CompareTag("seaweedShop"))
        {
            if (seaweedSellCoroutine != null)
            {
                StopCoroutine(seaweedSellCoroutine);
                seaweedSellCoroutine = null;
            }
        }
    }

    public IEnumerator CollectSeaweedOverTime()
    {
        while (true)
        {
            GameManager.instance.AddSeaweed(1);
            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator SellSeaweedOverTime()
    {
        while (true)
        {
            GameManager.instance.ConvertSeaweedToMoney();
            yield return new WaitForSeconds(1f);
        }
    }

    void OnMouseDown()
    {
        
        GameManager.instance.SelectUnit(this);
    }
}
