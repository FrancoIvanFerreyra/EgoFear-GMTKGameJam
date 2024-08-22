using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    Animator anim;
    public PlayerSwitch playerSwitch;
    public LiftSpace liftSpace;
    AudioSource source;
    public AudioClip footstep, hop, fracture;
    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSwitch.isPlayerActive)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        //Disable player movement is ego is active
        else
        {
            horizontalMove = 0;
            jump = false;
        }
    }

    void FixedUpdate()
    {
        //Only move player if is unlinked to ego
        if (!(!playerSwitch.isPlayerActive && liftSpace.playerIsOnTop))
        {
            // Move player
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

            anim.SetBool("isMoving", horizontalMove != 0);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collect coins
        if(collision.gameObject.tag == "Coin")
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            GoldManager.instance.AddGold(50);
        }
        
    }


    //Different sound states
    public void Footstep()
    {
        source.clip = footstep;
        source.Play();
    }

    public void Hop()
    {
        source.clip = hop;
        source.Play();
    }

    public void Fracture()
    {
        source.clip = fracture;
        source.Play();
    }

}
