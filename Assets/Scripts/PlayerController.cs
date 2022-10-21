using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRb;
    private Animator PlayerAnim;
    private AudioSource PlayerAudio;

    public AudioClip jumpSound;
    public AudioClip crashSound;


    public float jumpForce = 5.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    void Start()
    {
        Physics.gravity *= gravityModifier;
        PlayerRb = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
        {
            PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            PlayerAnim.SetTrigger("Jump_trig");
            PlayerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            PlayerAnim.SetBool("Death_b", true);
            PlayerAnim.SetInteger("DeathType_int", 1);
            PlayerAudio.PlayOneShot(crashSound, 1.0f);
        }

    }
}
