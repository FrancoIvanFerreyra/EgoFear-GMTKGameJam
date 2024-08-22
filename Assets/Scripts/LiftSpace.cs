using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSpace : MonoBehaviour
{
    public bool playerIsOnTop = false;
    public PlayerSwitch playerSwitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //If player is on top of ego and ego is active, link them
            if(!playerSwitch.isPlayerActive)
            {
                playerSwitch.LinkPlayerWithEgo();
            }
            playerIsOnTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOnTop = false;
        }
    }
}
