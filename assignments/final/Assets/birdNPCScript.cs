using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//have birds rotate 360 degrees in their position
//if player is detected using raytracing have the NPC go to that direction

public class birdScript : MonoBehaviour
{
    float rotateSpeed; 
    float detectionRange = 35f;
    float moveSpeed = 6f;

    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("player");

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
