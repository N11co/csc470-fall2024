using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workerTurtleNPCScript : MonoBehaviour
{
    
    public Coroutine crateCollectionCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("seaweedFarm"))
        {
            if (crateCollectionCoroutine == null)
            {
                crateCollectionCoroutine = StartCoroutine(CollectSeaweedOverTime());
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("seaweedFarm"))
        {
            if (crateCollectionCoroutine != null)
            {
                StopCoroutine(crateCollectionCoroutine);
                crateCollectionCoroutine = null;
            }
        }
    }

    public IEnumerator CollectSeaweedOverTime()
    {
        while (true)
        {
            GameManager.instance.AddSeaweedToCrate(1);
            yield return new WaitForSeconds(1f);
        }
    }

}
