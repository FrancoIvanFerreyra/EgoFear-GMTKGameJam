using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject launchPoint;
    public GameObject prefab;
    public int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        launchPoint = transform.GetChild(0).gameObject; 
    }

    public void LaunchEnemy()
    {
        //Spawns an enemy on launchPoint, facing at direction an adding a force to it
        GameObject newEnemy = Instantiate(prefab, null);
        newEnemy.transform.position = launchPoint.transform.position;
        newEnemy.GetComponent<Enemy>().direction = direction;
        newEnemy.GetComponent<Rigidbody2D>().AddForce(direction * Vector2.right, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Launch enemy when player hits the spawner from below
        if(collision.gameObject.tag == "Player")
        {
            if(transform.position.y > collision.transform.position.y)
            {
                //Enemy spawning costs 10 gold
                GoldManager.instance.QuitGold(10);
                LaunchEnemy();
            }
        }
    }
}
