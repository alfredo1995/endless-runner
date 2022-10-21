using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefabs;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    void Start()
    {
        InvokeRepeating("spawObstacle", startDelay, repeatRate);
    }

    void Update()
    {
        
    }

    void spawObstacle()
    {
        Instantiate(obstaclePrefabs, spawnPosition, obstaclePrefabs.transform.rotation);

    }
}
