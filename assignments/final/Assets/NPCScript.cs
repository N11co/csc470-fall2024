using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//have NPCs rotate 360 degrees on their position
//if player is detected using raytracing have the NPC go to that direction (make sure to implement delay)

public class NPCScript : MonoBehaviour
{
    //to detect player
    public float delay = 0.5f; 
    public bool isChasing = false;
    public float moveSpeed = 8f; 
    
    float rotateSpeed; 
    float detectionRange = 30f;

    public NavMeshAgent nma;

    //LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        //layerMask = LayerMask.GetMask("player");

        rotateSpeed = Random.Range(15,40);

        transform.Rotate(0, Random.Range(0,360), 0);

        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate continuously
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        //detect player
        DetectPlayer();
        
    }

    public void DetectPlayer()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 1.15f, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.up * 1.25f, transform.forward * detectionRange, Color.cyan);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("turtle") && !isChasing)
            {
                StartCoroutine(PlayerDetected());
            }
        }
    }

    public IEnumerator PlayerDetected()
    {
        isChasing = true;

        //delay
        yield return new WaitForSeconds(delay);

        //get player's last known position from GameManager
        Vector3 lastPlayerPos = GameManager.instance.GetLastPlayerPos();

        //move to last known location
        Debug.Log("Chasing the player to position: " + lastPlayerPos);

        if (nma != null)
        {
            nma.SetDestination(lastPlayerPos);
        }
        else
        {
            StartCoroutine(MoveToLastKnownPos(lastPlayerPos));
        }
        
        isChasing = false;
    }

    IEnumerator MoveToLastKnownPos(Vector3 tarPos)
    {
        while (Vector3.Distance(transform.position, tarPos) > 0.5f)
        {
            Vector3 direction = (tarPos - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }

    //void MoveToPlayer(Vector3 playerPos)
    //{
    //    //move crab NPC to player
    //    Vector3 direction = (playerPos - transform.position).normalized;
    //    transform.position += direction * moveSpeed * Time.deltaTime;
    //}
}
