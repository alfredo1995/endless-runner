using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float reapetWidth;
    void Start()
    {
        startPos = transform.position;
        reapetWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        if(transform.position.x < startPos.x - reapetWidth)
        {
            transform.position = startPos;
        }
    }
}
