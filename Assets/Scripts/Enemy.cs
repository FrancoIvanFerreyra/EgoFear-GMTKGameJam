using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRb;
    public float speed = 1;

    //Direction of enemy's walk (-1 left, 1 right)
    public int direction = -1;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.velocity = new(direction * speed, enemyRb.velocity.y);
        
        //Flip sprite by direction so he faces correctly
        spriteRenderer.flipX = direction != 1;
    }
}
