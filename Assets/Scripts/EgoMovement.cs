using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoMovement : MonoBehaviour
{

    public PlayerController controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public PlayerSwitch playerSwitch;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerSwitch.isPlayerActive)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        //Disables movement of ego when player is active
        else
        {
            horizontalMove = 0;
            jump = false;
        }
    }

    void FixedUpdate()
    {
        //Move ego
        controller.Move(horizontalMove * Time.fixedUnscaledDeltaTime, crouch, jump);
        jump = false;
        
        //Plays slide sound only if ego is active and is on the ground
        source.enabled = horizontalMove != 0 && controller.m_Grounded;
    }

}
