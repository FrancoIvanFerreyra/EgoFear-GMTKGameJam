using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerMovement player;
    public EgoMovement ego;
    public Rigidbody2D playerRb, egoRb;
    public LiftSpace liftSpace;
    public PhysicsMaterial2D nbnf;
    public bool isPlayerActive = true;
    public CinemachineVirtualCamera virtualCamera;
    AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Switch();
        }
    }

    void Switch()
    {
        //Quit gold from player
        GoldManager.instance.QuitGold(2);
        //PLay switch sound
        source.clip = clip;
        source.Play();

        //Switch to player
        if(isPlayerActive)
        {
            playerRb.mass *= 100000;
            
            //If player is on top of ego, link them
            if (liftSpace.playerIsOnTop)
            {
                Debug.Log("joint start");
                LinkPlayerWithEgo();
            }
            
            //Give back air control to ego
            egoRb.gameObject.GetComponent<PlayerController>().m_AirControl = true;
            egoRb.mass = 1;
            
            //Camera follows ego
            virtualCamera.Follow = GameObject.Find("Ego").transform;
            isPlayerActive = false;
        }
        //Switch to player
        else
        {
            //If player is on top, unlink them
            if (liftSpace.playerIsOnTop)
            {
                Debug.Log("joint end");
                UnlinkPlayerWithEgo();
            }
            
            //Quit air control to ego
            egoRb.gameObject.GetComponent<PlayerController>().m_AirControl = false;
            playerRb.mass = 1;
            egoRb.mass = 100000;
            
            //Camera follows player
            virtualCamera.Follow = GameObject.Find("Player").transform.transform;
            isPlayerActive = true;
        }

    }

    public void LinkPlayerWithEgo()
    {
        //Remove the player's rigidbody and make it a child of ego's gameobject so he follows him
        Destroy(playerRb);
        player.transform.SetParent(ego.transform);
    }

    public void UnlinkPlayerWithEgo()
    {
        //Remove player from ego's childs and add him a rigidbody with same properties
        player.transform.SetParent(null);
        playerRb = player.gameObject.AddComponent<Rigidbody2D>();
        playerRb.gravityScale = 1.7f;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRb.sharedMaterial = nbnf;
    }


}
