using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15.0f;
    private PlayerController playControllerScript;
    void Start()
    {
        playControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(playControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            
        }
        if(transform.position.x < -10 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
