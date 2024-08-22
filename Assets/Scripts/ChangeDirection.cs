using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Change enemy direction on wall collision or floor ending
        if(collision.gameObject.name == "Tilemap")
        {
            if(enemy.direction == -1)
            {
                enemy.direction = 1;
            }
            else
            {
                enemy.direction = -1;
            }
        }
    }
}
