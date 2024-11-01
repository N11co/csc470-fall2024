using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour

{
    //float moveSpeed = 1;
    float freq = 1.5f;
    float amp = 25;
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
        Vector3 pos = startPosition + Vector3.up * Mathf.Sin((offset + Time.time / 2) * freq) * amp;
        transform.position = pos;
    }
}