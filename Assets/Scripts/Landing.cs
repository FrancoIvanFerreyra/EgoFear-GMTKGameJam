using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Ego ego;
    Rigidbody2D rb;
    public int hopForce = 75;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void CheckFallDamage()
    {
        if(rb == null)
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }
        //If player falls too hard, ego shrinks
        if(Mathf.Abs(rb.velocity.y) > 8)
        {
            playerMovement.Fracture();
            ego.currentScale -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //Killing enemies makes ego grow
            if (transform.position.y > collider.gameObject.transform.position.y)
            {
                rb.AddForce(new Vector2(0f, Mathf.Abs(rb.velocity.y) * hopForce));
                playerMovement.Hop();
                collider.gameObject.SetActive(false);
                ego.currentScale += GetPoints(Mathf.Abs(rb.velocity.y));
            }

        }
    }

    float GetPoints(float velocity)
    {
        //Ego growing quantity is base on how fast the player fell into enemies
        float result = 0;
        if(velocity >= 0 && velocity <= 8)
        {
            result = 1;
        }
        else if (velocity > 8 && velocity <= 10)
        {
            result = 2;
        }
        else 
        {
            result = 3;
        }
        return  result;
    }
}
