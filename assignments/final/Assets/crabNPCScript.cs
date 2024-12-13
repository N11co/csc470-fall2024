using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//have crabs rotate 360 degrees on their position
//if player is detected using raytracing have the NPC go to that direction

public class crabNPCScript : MonoBehaviour
{

    //bool playerDetected = false;
    float rotateSpeed; 
    float detectionRange = 20f;
    float moveSpeed = 2f;
    //float rotationSpeed;
    LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("player");

        //GameManager.instance.units.Add(this);

        rotateSpeed = Random.Range(15,40);

        transform.Rotate(0, Random.Range(0,360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate continuously
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        
        //raycasting
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit; 

        Debug.DrawRay(transform.position + Vector3.up * 1.25f, transform.forward * detectionRange, Color.cyan);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("turtle"))
            {
                //playerDetected = true;
                MoveToPlayer(hit.collider.transform.position);
            }
        }
    }

    void MoveToPlayer(Vector3 playerPos)
    {
        //move crab NPC to player
        Vector3 direction = (playerPos - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
