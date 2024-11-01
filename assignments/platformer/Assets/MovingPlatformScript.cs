using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    //float moveSpeed = 1;
    float freq = 2;
    float amp = 10;
    float offset = 10;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        //offset = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = startPosition + Vector3.forward * Mathf.Sin((offset + Time.time / 2) * freq) * amp;
        transform.position = pos;
    }
}