using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    //public string objective;    

    public string unitName;

    public string bio;

    public string stats;

    public NavMeshAgent nma;

    public Renderer bodyRenderer;

    public bool selected = false;

    public Color normalColor;

    public Color selectedColor;

    LayerMask layerMask;

    public Vector3 destination;

    

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("wall");
    
        GameManager.instance.units.Add(this);
    }

    void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if (destination != null)
        //{
        //    Vector3 direction = destination - transform.position;
        //    direction.Normalize();
        //    transform.position += direction * 5 * Time.deltaTime;
        //}

        Vector3 rayStart = transform.position + Vector3.up * .25f;
        

        Color rayColor = Color.black;
        RaycastHit hit;
        if (Physics.Raycast(rayStart, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            //we hit something
            if (hit.collider.gameObject.GetComponent<UnitScript>().unitName == "Buster") {
                rayColor = Color.cyan;
            }
        }
        Debug.DrawRay(rayStart, transform.forward * 4, rayColor);
        
    }

    void OnMouseDown()
    {
        selected = true;
        bodyRenderer.material.color = selectedColor;
        GameManager.instance.OpenCharacterSheet(this);
    }
}
